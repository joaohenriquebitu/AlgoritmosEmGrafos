# AlgoritmosEmGrafos

Para rodar em um ambiente simplificado, como por exemplo um ambiente online, pode se utilizar: https://dotnetfiddle.net

Apenas é necessário alterar a linha de atribuição da string[] linhas passando o texto do arquivo diretamente. Como por exemplo:
        string[] linhas = new string[]
{
    "6",
    "6",
    "0 1 5",
    "0 2 -2",
    "1 3 3",
    "2 3 4",
    "3 4 -1",
    "4 5 2",
    "0",
    "5",
    "1 4"
};

# ------------------------------------
# Exemplo de saída esperada:
# ------------------------------------

--- Rodada 1 ---
Ladrão foi para o vértice 2.
Equipe policial 1 foi para o vértice 1.
Equipe policial 2 foi para o vértice 4.

--- Rodada 2 ---
Ladrão foi para o vértice 3.
Equipe policial 1 foi para o vértice 3.
Equipe policial 2 foi para o vértice 4.

A polícia alcançou o ladrão!

---------------- RELATÓRIO FINAL ------------------
Status: PRESO
Total de Rodadas: 2
Equipes de Polícia Ativas: 2
---------------------------------------------------
