using System.Collections.Generic;
using System.IO;

using E.Expressions;
using E.Parsing;

namespace E.Compilation.Tests;

public class CSharpRewriterTests
{
    [Fact]
    public void Percent()
    {
        Assert.Equal(
            "object a = 2 * (50 * 0.01);",
            Rewrite("let a = 2 * 50%")
        );
    }

    [Fact]
    public void Access3()
    {
        Assert.Equal(
            "a[0].B[1].C[2]",
            Rewrite("a[0].b[1].c[2]")
        );
    }

    [Fact]
    public void Access2()
    {
        Assert.Equal(
            "a[0][1][2][3][4][5]",
            Rewrite("a[0][1][2][3][4][5]")
        );
    }

    [Fact]
    public void Access1()
    {
        Assert.Equal(
            "a.B.C.D.E.F.G.H",
            Rewrite("a.b.c.d.e.f.g.h")
        );
    }

    [Fact]
    public void RewritePipe()
    {
        Assert.Equal(
            expected: "D(C(B(A, 100), 17))",
            actual: Rewrite("A |> B(100) |> C(17) |> D")
        );
    }

    // TODO: JavaScript...

    // [Fact]
    public void RewriteObserver()
    {
        Assert.Equal(
            """
            class Gallery extends HTMLElement {
              constructor(slides) {
                super();

                this.slides = slides;
                
                this.addEventListener("pointerpressed", this.onPointerPressed.bind(this));
              }
               
              onPointerPressed(press) { 
                var gallaryPointerMove = function () {
                  console.log("moved");
                }.bind(this);

                this.addEventListener("pointermoved", gallaryPointermove, false);

                this.document.addEventListener("pointerreleased", () => {
                  this.removeEventListener("pointermoved", gallaryPointerMove, false);
                }.bind(this), { once: true });
              }

              connectedCallback() {
                console.log("attached");
              }
            }

            customElements.define("Gallery", Gallery);
            """,

        Rewrite(
            """
            Gallery impl {
              from (slides: Slide[]) => Gallary { slides }

              on attached { 
               console.log("attached")
              }

              on Pointer::pressed {
                observe gallary Pointer 'move {
                  log "moved"

                  // Drag the slide

                } until gallary.Root Pointer::released
              }
            }
            """));
    }

    [Fact]
    public void RewriteTypeInitialization()
    {
        Assert.Equal(
            """
            new Account(balance: 100, owner: "me", created: new Date(year: 2000, month: 1, day: 1))
            """,

        Rewrite(
            """
            Account(
              balance : 100,
              owner   : "me",
              created : Date(year: 2000, month: 01, day: 01)
            )
            """));
    }

    [Fact]
    public void Ternary()
    {
        Assert.Equal(
                    "x < 0.5 ? Math.Pow(x * 2, 3) / 2 : (Math.Pow((x - 1) * 2, 3) + 2) / 2",
            Rewrite("x < 0.5 ? ((x * 2) ** 3) / 2 : ((((x - 1) * 2) ** 3) + 2) / 2"));
    }

    [Fact]
    public void SingleStatements()
    {
        Assert.Equal("long a = 100;", Rewrite("let a: Int64 = 100"));
        Assert.Equal("long a = 100;", Rewrite("let a: i64 = 100"));
        Assert.Equal("int a = 100;", Rewrite("let a: Int32 = 100"));
        Assert.Equal("decimal a = 100;", Rewrite("let a: Decimal = 100"));
        Assert.Equal("double a = 1;", Rewrite("var a: Float64 = 1.0"));
        Assert.Equal("double a = 1.9;", Rewrite("var a: Number = 1.9"));
        Assert.Equal("double a = 1.9;", Rewrite("var a = 1.9"));

        Assert.Equal("string s = \"hi\";", Rewrite("let s = \"hi\""));
    }

    [Fact]
    public void IfElseIfElse()
    {
        Assert.Equal("""
            if (a > 5)
            {
                return 3;
            }
            else if (a > 4)
            {
                return 4;
            }
            else
            {
                return 5;
            }
            """.Replace("\r\n", "\n"),

        Rewrite(
            """
            if a > 5 { 
              return 3
            }
            else if a > 4 {
              return 4
            }
            else {
              return 5
            }

            """));
    }

    [Fact]
    public void SwitchStatement()
    {
        Assert.Equal("""
            return a switch
            {
                1 => 1 + 1,
                2 => 1 - 2,
                3 => 1 * 3,
                4 => 1 / 4,
                5 => 1 % 5,
                6 => Math.Pow(1, 6),
                7 => Math.Abs(x) + 32 / 3
            }
            """.ReplaceLineEndings("\n"),

        Rewrite(
            """
            match a {
              1 => 1 + 1
              2 => 1 - 2
              3 => 1 * 3
              4 => 1 / 4
              5 => 1 % 5
              6 => 1 ** 6,
              7 => abs(x) + 32 / 3
            }
            """));
    }

    [Fact]
    public void SwitchTypeStatement()
    {
        Assert.Equal(
            """
            object width;

            switch (media)
            {
                case Image image: width = image.Width; break;
                case Video video: width = video.Width; break;
                default: width = 0; break;
            }
            """.ReplaceLineEndings("\n"),

        Rewrite(
            """
            let width = match media {
              (image: Image) => image.width;
              (video: Video) => video.width;
              _              => 0
            }
            """));
    }

    public static string Rewrite(string source)
    {
        var compiler = new Compiler();

        var parser = new Parser(source);

        var expressions = new List<IExpression>();

        while (parser.TryReadNext(out var syntax))
        {
            expressions.Add(compiler.Visit(syntax));
        }

        using var writer = new StringWriter();

        var csharp = new CSharpEmitter(writer);

        csharp.Visit(expressions);

        return writer.ToString();
    }
}