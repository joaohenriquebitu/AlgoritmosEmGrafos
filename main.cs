using System;
using System.Collections.Generic;


public class Aresta
{
    public int Destino { get; set; }
    public int Peso { get; set; }
    public Aresta(int destino, int peso) { Destino = destino; Peso = peso; }
}


public class Grafo
{
    public int TotalVertices { get; }
    public Dictionary<int, List<Aresta>> Adj = new Dictionary<int, List<Aresta>>();
    public Dictionary<int, List<Aresta>> Rev = new Dictionary<int, List<Aresta>>();

    public Grafo(int vertices)
    {
        TotalVertices = vertices;
        for (int i = 0; i < TotalVertices; i++)
        {
            Adj[i] = new List<Aresta>();
            Rev[i] = new List<Aresta>();
        }
    }

    public void AddCaminho(int u, int v, int peso)
    {
        Adj[u].Add(new Aresta(v, peso));
        Rev[v].Add(new Aresta(u, peso));
    }
}



public class Program
{
    const int inf = int.MaxValue / 2;

    public static (int[] dist, int[] direcao) BellmanFord(int n, Dictionary<int, List<Aresta>> adjacencias, int origem)
    {
        int[] dist = new int[n];
        int[] direcao = new int[n];

        for (int i = 0; i < n; i++)
        {
            dist[i] = inf;
            direcao[i] = -1;
        }
        dist[origem] = 0;

        for (int i = 1; i < n; i++)
        {
            foreach (var u in adjacencias.Keys)
            {
                foreach (var aresta in adjacencias[u])
                {
                    if (dist[u] + aresta.Peso < dist[aresta.Destino])
                    {
                        dist[aresta.Destino] = dist[u] + aresta.Peso;
                        direcao[aresta.Destino] = u;
                    }
                }
            }
        }
        return (dist, direcao);
    }

public static void Main()
    {
      //leitura do arquivo

        string[] linhas = File.ReadAllLines("..\\..\\..\\config.txt");
        
        int numVertices = int.Parse(linhas[0]);
        Grafo ilha = new Grafo(numVertices);

        int qtdArestas = int.Parse(linhas[1]);
        int idx = 2;

        for (int i = 0; i < qtdArestas; i++)
        {
            string[] dados = linhas[idx++].Split(' ');
            ilha.AddCaminho(int.Parse(dados[0]), int.Parse(dados[1]), int.Parse(dados[2]));
        }

        int posLadrao = int.Parse(linhas[idx++]);

        string[] dadosPortos = linhas[idx++].Split(' ');
        List<int> portos = new List<int>();
        foreach (var p in dadosPortos) portos.Add(int.Parse(p));

        string[] dadosPolicias = linhas[idx++].Split(' ');
        List<int> policias = new List<int>();
        foreach (var p in dadosPolicias) policias.Add(int.Parse(p));

        int rodada = 0;
        bool capturado = false;

        // planejamento do ladrão
        var (custos, caminhos) = BellmanFord(ilha.TotalVertices, ilha.Adj, posLadrao);
        
        int portoAlvo = -1;
        int menorCusto = inf;
        foreach (int porto in portos)
        {
            if (custos[porto] < menorCusto)
            {
                menorCusto = custos[porto];
                portoAlvo = porto;
            }
        }
        
        var rotaFuga = new List<int>();
        for (int v = portoAlvo; v != -1; v = caminhos[v]) rotaFuga.Add(v);
        rotaFuga.Reverse();
        int passoLadrao = 1; 

        // while principal
        while (true)
        {
            rodada++;
            Console.WriteLine($"--- Rodada {rodada} ---");

            // movimentação do ladrão
            if (passoLadrao < rotaFuga.Count) {
                posLadrao = rotaFuga[passoLadrao++];
                Console.WriteLine($"Ladrão foi para o vértice {posLadrao}.");
            }

            if (portos.Contains(posLadrao)) break;
            
            // verifica se foi capturado
            foreach (int p in policias) if (p == posLadrao) capturado = true;
            if (capturado) break;

            // movimentação da polícia
            var (_, mapaDeDirecoes) = BellmanFord(ilha.TotalVertices, ilha.Rev, posLadrao);

            for (int i = 0; i < policias.Count; i++)
            {
                for (int m = 0; m < 2; m++)
                {
                    if (policias[i] == posLadrao) break;
                    int proximo = mapaDeDirecoes[policias[i]];
                    if (proximo != -1) policias[i] = proximo;
                }
                Console.WriteLine($"Equipe policial {i + 1} foi para o vértice {policias[i]}.");
                if (policias[i] == posLadrao) capturado = true;
            }

            if (capturado) break;
            Console.WriteLine();
        }

        // relatório final
        Console.WriteLine(capturado ? "\nA polícia alcançou o ladrão!" : "\nO ladrão alcançou um porto de saída!");

        Console.WriteLine("\n---------------- RELATÓRIO FINAL ------------------");
        Console.WriteLine($"Status: {(capturado ? "PRESO" : "ESCAPOU")}");
        Console.WriteLine($"Total de Rodadas: {rodada}");
        Console.WriteLine($"Equipes de Polícia Ativas: {policias.Count}");
        Console.WriteLine("---------------------------------------------------");
    }
}
