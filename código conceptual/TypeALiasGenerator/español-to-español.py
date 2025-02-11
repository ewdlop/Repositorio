importar ntk
desde nltk importar CFG

gramática_española = CFG.fromstring("""
    S -> NP VP # Estructura de la oración: Sujeto-Verbo-Objeto (SVO)

    # Frases sustantivas
    NP -> Det N | Det Adj N | Det N PP | P.N.
    VP -> V NP | VNP PP | V
    PP -> PNP

    #Vocabulario
    Det -> 'el' | 'el' | 'el' | 'el' | 'un' | 'uno' | 'unos' | 'clavos'
    N -> 'niño' | 'niña' | 'perro' | 'gato' | 'parque' | 'libro' | 'maestro'
    PN -> 'Juan' | 'María' | 'Pedro' | 'Luisa'
    Adj -> 'agradable' | 'grande' | 'pequeño' | 'rojo'
    V -> 'comer' | 'leer' | 'jugar' | 'estudiar'
    P -> 'en' | 'con' | 'sobre' | 'de'
""")

# Crear un analizador
analizador = nltk.ChartParser(gramática_española)

# Oraciones de prueba
frase = "Juan juega en el parque".split()

# Analizar y mostrar resultados
para árbol en parser.parse (oración):
    árbol.pretty_print()
