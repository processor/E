module Medicine

Disease protocol {
  treat() -> Treatment
}


// A disease takes actions against an organism

Disease actor {

}


// A diease may be treated

// IDC-10 (14,400 codes)
// -----------------------
// A General and unspecified
// B Blood, blood forming organs, lymphatics, spleen
// D Digestive
// F Eye
// H Ear
// K Circulatory
// L Musculoskeletal
// N Neurological
// P Psychological
// R Respiratory
// S Skin
// T Endocrine, metabolic and nutritional
// U Urology
// W Pregnancy, childbirth, family planning
// X Female genital system and breast
// Y Male genital system
// Z Social problems
//  
// BloodVolume,
// CD4CellCount,
// CBC,


Cancer protocol { 
  * | chemotherapy
    | radition
    ↺ : treatment
  * resolve ∎ : remission
}

  Acute `Disseminated `Encephalomyelitis
, Acute `necrotizing `hemorrhagic `leukoencephalitis
, Addisons `disease
, Agammaglobulinemia
, Alopecia `areata
, Amyloidosis
, Ankylosing `spondylitis
, AntiGBM `nephritis
, Autoimmune `angioedema
, Autoimmune `aplastic `anemia
, Autoimmune `dysautonomia
, Autoimmune `hepatitis
, Autoimmune `hyperlipidemia
, Autoimmune `immunodeficiency
, Autoimmune `inner `ear `disease
, Autoimmune `myocarditis
, Autoimmune `oophoritis
, Autoimmune `pancreatitis
, Autoimmune `retinopathy
, Autoimmune `thrombocytopenic `purpura
, Autoimmune `thyroid `disease
, Autoimmune `urticaria
, Axonal `& `neuronal `neuropathies
, Balo `disease
, Behcet’s `disease
, Bullous `pemphigoid
, Cardiomyopathy
, Castleman `disease
, Celiac `disease
, Chagas `disease
, Chronic `inflammatory `demyelinating `polyneuropathy
, Chronic `recurrent `multifocal `ostomyelitis
, Cicatricial `pemphigoid/benign `mucosal `pemphigoid
, Crohn’s `disease
, Cold `agglutinin `disease
, Congenital `heart `block
, Coxsackie `myocarditis
, CREST `disease
, Essential `mixed `cryoglobulinemia
, Demyelinating `neuropathies
, Dermatitis `herpetiformis
, Dermatomyositis
, Neuromyelitis `optica
, Discoid `lupus
, Endometriosis
, Eosinophilic `esophagitis
, Eosinophilic `fasciitis
, Erythema `nodosum
, Experimental `allergic `encephalomyelitis
, Fibromyalgia**
, Fibrosing `alveolitis
, Giant `cell `arteritis `(temporal `arteritis)
, Giant `cell `myocarditis
, Glomerulonephritis
, Granulomatosis `with `Polyangiitis `(GPA)
, Graves’ `disease
, Hashimoto’s `encephalitis
, Hashimoto’s `thyroiditis
, Hemolytic `anemia
, Henoch-Schonlein `purpura
, Herpes `gestationis
, Hypogammaglobulinemia
, Idiopathic `thrombocytopenic `purpura `(ITP)
, IgA `nephropathy
, IgG4-related `sclerosing `disease
, Immunoregulatory `lipoproteins
, Inclusion `body `myositis
, Interstitial `cystitis
, Juvenile `arthritis
, Juvenile `diabetes `(Type `1 `diabetes)
, Juvenile `myositis
, Leukocytoclastic `vasculitis
, Lichen `planus
, Lichen `sclerosus
, Ligneous `conjunctivitis
, Linear `IgA `disease
, Lupus `(SLE)
, Lyme `disease, `chronic
, Meniere’s `disease
, Microscopic `polyangiitis
, Mixed `connective `tissue `disease
, Mooren’s `ulcer
, Mucha-Habermann
, Multiple `sclerosis
, Myasthenia `gravis
, Myositis
, Narcolepsy
, Neuromyelitis `optica
, Neutropenia
, Ocular `cicatricial `pemphigoid
, Optic `neuritis
, Palindromic `rheumatism
, Pediatric `Autoimmune `Neuropsychiatric `Disorders `Associated `with `Streptococcus
, Paraneoplastic `cerebellar `degeneration
, Paroxysmal `nocturnal `hemoglobinuria
, }eripheral `uveitis
, Pemphigus
, Peripheral `neuropathy
, Perivenous `encephalomyelitis
, Pernicious `anemia
, Polyarteritis `nodosa
, Polymyalgia `rheumatica
, Polymyositis
, Progesterone `dermatitis
, Primary `biliary `cirrhosis
, Primary `sclerosing `cholangitis
, Psoriasis
, Psoriatic `arthritis
, Idiopathic `pulmonary `fibrosis
, Pyoderma `gangrenosum
, Pure `red `cell `aplasi
, Raynauds `phenomenon
, Reactive `Arthritis
, Reflex `sympathetic `dystrophy
, Relapsing `polychondritis
, Retroperitoneal `fibrosis
, Rheumatic `fever
, Rheumatoid `arthritis
, Sarcoidosis
, Scleritis
, Scleroderma
, Subacute `bacterial `endocarditis
, Sympathetic `ophthalmia
, Takayasu’s `arteritis
, Temporal `arteritisGiant `cell `arteritis
, Thrombocytopenic `purpura
, Transverse `myelitis
, Type `1 `diabetes
, Ulcerative `colitis
, Undifferentiated `connective `tissue `disease
, Uveitis
, Vasculitis
, Vesiculobullous `dermatosis
, Vitiligo 
: Disease

ADEM    ≡ Acute `Disseminated `Encephalomyelitis
PANDAS  ≡ Pediatric `Autoimmune `Neuropsychiatric `Disorders `Associated `with `Streptococcus 
APS     ≡ Antiphospholipid `syndrome
ATP     ≡ Autoimmune `thrombocytopenic `purpura
AIED    ≡ Autoimmune `inner `ear `disease
CIDP    ≡ Chronic `inflammatory `demyelinating `polyneuropathy
CRMO    ≡ Chronic `recurrent `multifocal `ostomyelitis 
MCTD    ≡ Mixed `connective `tissue `disease 
PANDAS  ≡ Pediatric `Autoimmune `Neuropsychiatric `Disorders Associated with Streptococcus 
PNH     ≡ Paroxysmal `nocturnal `hemoglobinuria 
SBE     ≡ Subacute `bacterial `endocarditis
TTP     ≡ Thrombocytopenic `purpura
UCTD    ≡ Undifferentiated `connective `tissue `disease