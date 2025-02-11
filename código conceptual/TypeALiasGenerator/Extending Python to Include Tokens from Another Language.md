# Extending Python to Include Tokens from Another Language

### **Extending Python to Include Tokens from Another Language**
If you want to **extend Python’s tokenizer** to support **tokens from another language**, you can modify the **Python lexer** or use **NLTK**, `tokenize`, or `re` to handle multilingual token recognition.

---

## **1️⃣ Using `tokenize` to Modify Python Token Handling**
Python’s built-in **`tokenize` module** allows for **customizing token recognition**.

📌 **Example: Extending Python to recognize Spanish keywords**
```python
import tokenize
from io import BytesIO

# Define a mapping of Spanish keywords to Python keywords
SPANISH_KEYWORDS = {
    "imprimir": "print",
    "definir": "def",
    "mientras": "while",
    "para": "for",
    "si": "if",
    "sino": "else",
    "importar": "import"
}

def translate_spanish_python(code):
    """Convert Spanish Python code to standard Python."""
    tokens = []
    for tok in tokenize.tokenize(BytesIO(code.encode('utf-8')).readline):
        token_text = tok.string
        if token_text in SPANISH_KEYWORDS:
            token_text = SPANISH_KEYWORDS[token_text]  # Replace with Python equivalent
        tokens.append(token_text)
    return " ".join(tokens)

# Example Spanish-like Python code
spanish_code = """
imprimir("Hola Mundo")
definir funcion():
    mientras True:
        imprimir("Ejecutando...")
"""

# Convert Spanish Python to standard Python
translated_code = translate_spanish_python(spanish_code)
exec(translated_code)  # Runs the translated Python code
```
✅ **Advantages:**  
✔ Allows **custom syntax** in another language  
✔ Works with **any programming construct**  

🚨 **Disadvantage:**  
- Requires defining **all tokens manually**.  

---

## **2️⃣ Using `re` to Extend Python’s Token Recognition**
If you want to allow **multilingual tokens dynamically**, you can use **regex (`re`)** to tokenize input and replace foreign words.

📌 **Example: Adding Spanish and English tokens dynamically**
```python
import re

# Define language tokens (multi-language support)
TOKEN_MAP = {
    "es": {"imprimir": "print", "si": "if", "sino": "else"},
    "fr": {"imprimer": "print", "si": "if", "sinon": "else"}
}

def translate_code(code, lang="es"):
    """Replace non-Python tokens with Python equivalents."""
    if lang not in TOKEN_MAP:
        return code  # Return original if language is not supported
    for word, replacement in TOKEN_MAP[lang].items():
        code = re.sub(r'\b' + word + r'\b', replacement, code)  # Replace whole words
    return code

# Spanish-like Python code
spanish_code = """
imprimir("Hola Mundo")
si True:
    imprimir("Condición cumplida")
sino:
    imprimir("Condición fallida")
"""

# Translate and execute
python_code = translate_code(spanish_code, lang="es")
exec(python_code)  # Runs the translated code
```
✅ **Advantages:**  
✔ Dynamically handles multiple languages  
✔ Works for **real-time replacement**  

🚨 **Disadvantage:**  
- **Not a full parser**, just replaces tokens.

---

## **3️⃣ Modify Python’s `tokenize` Directly for Custom Tokens**
You can **override Python’s tokenizer** using `tokenize` to introduce **custom tokens**.

📌 **Example: Custom Tokenizer to Recognize Mixed-Language Tokens**
```python
import tokenize
import io

code = "imprimir('Hola')\npara i en rango(5): imprimir(i)"

# Custom token replacement (Spanish -> Python)
SPANISH_TO_PYTHON = {
    "imprimir": "print",
    "para": "for",
    "en": "in",
    "rango": "range"
}

def custom_tokenizer(code):
    """Tokenize and replace Spanish tokens with Python equivalents."""
    result = []
    stream = io.BytesIO(code.encode('utf-8')).readline
    tokens = tokenize.tokenize(stream)

    for toknum, tokval, _, _, _ in tokens:
        if tokval in SPANISH_TO_PYTHON:
            result.append(SPANISH_TO_PYTHON[tokval])  # Replace Spanish tokens
        else:
            result.append(tokval)
    
    return " ".join(result)

# Run translated Python code
translated_code = custom_tokenizer(code)
exec(translated_code)
```
✅ **Advantages:**  
✔ Works at **tokenization level**  
✔ More **robust than regex-based solutions**  

🚨 **Disadvantage:**  
- Slightly **slower** than simple `re.sub()` replacement.

---

## **4️⃣ Modify AST (Abstract Syntax Tree) for True Multi-Language Support**
If you want **true integration of tokens into Python's syntax**, modify the **AST (Abstract Syntax Tree)**.

📌 **Example: Using `ast` to Convert Foreign Tokens**
```python
import ast

SPANISH_SYNTAX = {
    "imprimir": "print",
    "suma": "+",
    "resta": "-"
}

def translate_ast(node):
    """Recursively replace Spanish syntax in AST."""
    if isinstance(node, ast.Call) and isinstance(node.func, ast.Name):
        if node.func.id in SPANISH_SYNTAX:
            node.func.id = SPANISH_SYNTAX[node.func.id]  # Replace Spanish function names
    return node

# Example AST-based conversion
code = "imprimir(5 suma 3)"  # Spanish-like syntax
tree = ast.parse(code, mode='exec')

# Modify AST and execute
modified_tree = ast.fix_missing_locations(ast.NodeTransformer().visit(tree))
exec(compile(modified_tree, filename="<ast>", mode="exec"))
```
✅ **Advantages:**  
✔ Allows **true syntax transformation**  
✔ No need for manual string replacements  

🚨 **Disadvantage:**  
- **Requires AST knowledge**  
- More **complex** than token replacement.

---

## **📌 Summary: Best Approach for Adding Foreign Tokens**
| Method | Best For | Pros | Cons |
|--------|---------|------|------|
| **Regex Replacement (`re.sub`)** | Simple token replacement | Fast, easy | Doesn't enforce syntax rules |
| **`tokenize` API Override** | Customizing Python tokenizer | More control | Slightly complex |
| **AST Modification (`ast.parse`)** | True Python syntax modification | Native Python integration | Requires AST knowledge |
| **Translation before Execution** | Running non-Python syntax | Works for many languages | Not true token modification |

---

## **🚀 Final Recommendation**
- **If you just want token replacement**, use **Regex (`re`)**.  
- **If you need real Python syntax modification**, use **`tokenize`**.  
- **For full syntax changes**, use **AST-based translation**.  

Would you like an **even deeper integration**, like **making a full Python dialect**? 🤔🔥
