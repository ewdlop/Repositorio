A mathematically rigorous definition of **"Especialmente Forma Normal de la Gramática del Español para el Usuario"** (Especially Normal Form of Spanish Grammar for the User) can be formulated using formal language theory and mathematical logic.

---

### **Definition: Especially Normal Form (ENF) for Spanish Grammar**
Let \( \mathcal{L} \) be the set of all well-formed Spanish sentences, and let \( G \) be a **context-free grammar (CFG)** that generates \( \mathcal{L} \). We define the **Especially Normal Form (ENF)** as a restricted subset of CFGs that adheres to a standardization principle, ensuring syntactic correctness, minimal ambiguity, and compliance with prescriptive grammatical rules.

#### **1. Formal Representation**
A **Spanish CFG** is a tuple \( G = (V, \Sigma, R, S) \), where:
- \( V \) is a finite set of **non-terminal symbols** (e.g., *Sujeto, Verbo, Objeto*).
- \( \Sigma \) is a finite set of **terminal symbols** (e.g., words in the Spanish lexicon).
- \( R \) is a finite set of **production rules** in the form:
  \[
  A \rightarrow \alpha, \quad A \in V, \quad \alpha \in (V \cup \Sigma)^*
  \]
- \( S \in V \) is the **start symbol** (usually **S** for "sentence").

#### **2. Especially Normal Form Constraints**
A CFG \( G \) is said to be in **Especially Normal Form (ENF)** if and only if its production rules satisfy the following constraints:

1. **Canonical Subject-Verb-Object (SVO) Structure**  
   Every derivation must follow the standard Spanish **SVO** order when applicable:
   \[
   S \rightarrow NP \; VP
   \]
   \[
   VP \rightarrow V \; NP
   \]
   where:
   - \( NP \) (Noun Phrase) represents the subject.
   - \( VP \) (Verb Phrase) contains the verb and object.
   - \( V \) (Verb) is a finite verb.

2. **Minimal Ambiguity**  
   If \( G_1 \) and \( G_2 \) are two grammars generating the same sentence, then \( G_1 \) is in ENF if it has fewer ambiguous derivations:
   \[
   \forall x \in \mathcal{L}, \quad \text{ambiguity}(G_1, x) \leq \text{ambiguity}(G_2, x)
   \]

3. **Morphological Consistency**  
   If a sentence \( x \) contains a verb phrase, it must conform to grammatical conjugation rules:
   \[
   \forall x \in \mathcal{L}, \quad \exists v \in \text{Verbos} \quad \text{such that} \quad v \text{ agrees with its subject in number and person.}
   \]
   Example:
   - **Not in ENF**: *Los niños juega en el parque.*  
   - **In ENF**: *Los niños juegan en el parque.*

4. **No Redundant Pronouns (Clitic Redundancy Rule)**  
   If a noun phrase is explicitly stated, an additional redundant pronoun must be eliminated:
   \[
   S \rightarrow NP \; VP, \quad \text{but not } S \rightarrow NP \; Pro \; VP
   \]
   where \( Pro \) is an unnecessary pronoun.
   - **Not in ENF**: *El libro que yo lo compré es interesante.*  
   - **In ENF**: *El libro que compré es interesante.*

5. **Lexical Selection Consistency**  
   The choice of function words must conform to grammatical correctness:
   \[
   \forall w \in \text{lexicon}, \quad \text{if } w' \text{ is a grammatical alternative}, \quad \min_{\text{prescriptive correctness}}(w, w')
   \]
   Example:
   - **Not in ENF**: *Hubieron muchas personas.*  
   - **In ENF**: *Hubo muchas personas.*

6. **Transformation to Chomsky Normal Form (CNF)**
   Every production in \( R \) must be transformable into Chomsky Normal Form (CNF):
   \[
   A \rightarrow BC \quad \text{or} \quad A \rightarrow a, \quad \text{where } A, B, C \in V, \quad a \in \Sigma
   \]

### **Conclusion**
A grammar \( G \) is in **Especially Normal Form (ENF)** if and only if:
1. It adheres to canonical Spanish SVO ordering.
2. It minimizes syntactic ambiguity.
3. It enforces correct verb agreement.
4. It eliminates redundant clitic pronouns.
5. It maintains lexical selection correctness.
6. It can be transformed into Chomsky Normal Form (CNF).

This **Especially Normal Form** (ENF) ensures that Spanish grammar follows a mathematically precise and computationally analyzable structure while adhering to prescriptive linguistic rules. 🚀
