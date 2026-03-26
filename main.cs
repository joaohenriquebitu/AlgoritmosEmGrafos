using System;
using System.Collections.Generic;

class E {
    public int v, w; 
}

class Grafo {
    public Dictionary<int, List<E>> adj = new Dictionary<int, List<E>>();
    public void Add(int u, int v, int w) {
        if (!adj.ContainsKey(u)) adj[u] = new List<E>();
        if (!adj.ContainsKey(v)) adj[v] = new List<E>();
        adj[u].Add(new E { v = v, w = w });
    }
}

public class Program {
    static float inf = float.PositiveInfinity;

    public static (float[] distancias, int[] predecessores) BellmanFord(Grafo g, int origem) {
        int n = 0;
        foreach (var k in g.adj.Keys) n = Math.Max(n, k + 1);

        float[] d = new float[n];
        int[] p = new int[n];

        for (int i = 0; i < n; i++) {
            d[i] = inf;
            p[i] = -1;
        }
        d[origem] = 0;

        for (int i = 1; i < n; i++) {
            foreach (var u in g.adj.Keys) {
                foreach (var aresta in g.adj[u]) {
                    int v = aresta.v;
                    int w = aresta.w;

                    if (d[u] + w < d[v]) {
                        d[v] = d[u] + w;
                        p[v] = u;
                    }
                }
            }
        }

        foreach (var u in g.adj.Keys) {
            foreach (var aresta in g.adj[u]) {
                if (d[u] + aresta.w < d[aresta.v]) {
                    Console.WriteLine("ciclo negativo detectado em " + u);
                }
            }
        }

        return (d, p);
    }

    public static void Main() {
        var g = new Grafo();
        g.Add(0, 1, -1);
        g.Add(0, 2, 4);
        g.Add(1, 2, 3);
        g.Add(1, 3, 2);
        g.Add(1, 4, 2);
        g.Add(4, 3, -3);

        var (dist, pred) = BellmanFord(g, 0);

        for (int i = 0; i < dist.Length; i++) {
            Console.WriteLine("v" + i + ": d=" + dist[i] + " p=" + pred[i]);
        }
    }
}
