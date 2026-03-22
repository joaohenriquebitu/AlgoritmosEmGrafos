using System;
using System.Collections.Generic;

public class Program
{

    // algoritmo de bellman-ford, retornando também o caminho por meio dos predecessores
    private (int[] distancias, int[] predecessores) BellmanFord(int origem, bool usarGrafoReverso = false, /*grafo grafo*/)
        {
            // int n = grafo.numVertices
            int[] dist = new int[n];
            int[] pred = new int[n];

            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue / 2;
                pred[i] = -1;
            }
            dist[origem] = 0;

            for (int i = 1; i < n; i++)
            {
                foreach (var u in adjacencias.Keys)
                {
                    foreach (var aresta in adjacencias[u])
                    {
                        int v = aresta.Destino;
                        int peso = aresta.Peso;

                        if (dist[u] + peso < dist[v])
                        {
                            dist[v] = dist[u] + peso;
                            pred[v] = u;
                        }
                    }
                }
            }

            return (dist, pred);
        }
    public static void Main()
    {






        
      //
    }
}
