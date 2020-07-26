# Graph

## Introduction

This repository contains an implementation of various types of *graphs* with the collection of graph *algorithms* made for educational purposes.

## Graphs

The following types of graphs are implemented:

- **Undirected graph**
  - [unweighted](src/Graph/UndirectedGraph.cs)
  - [weighted](src/Graph/UndirectedWeightedGraph.cs)

- **Directed graph**
  - [unweighted](src/Graph/DirectedGraph.cs)
  - [weighted](src/Graph/DirectedWeightedGraph.cs)

- **Mixed graph**
  - [unweighted](src/Graph/MixedGraph.cs)
  - [weighted](src/Graph/MixedWeightedGraph.cs)

## Algorithms

The following graph algorithms are implemented:

- **Traversal/searching**
  - [Depth-first search (DFS)](src/Graph/Algorithms/DepthFirstSearch.cs)
  - [Breadth-first search (BFS)](src/Graph/Algorithms/BreadthFirstSearch.cs)

- **Finding shortest distances**
  - [Dijkstra's algorithm](src/Graph/Algorithms/Dijkstra.cs)
  - [Floyd-Warshall's algorithm](src/Graph/Algorithms/FloydWarshall.cs)
  - [Lee's algorithm](src/Graph/Algorithms/Lee.cs)

- **Finding the graph's spanning tree of minimal weight**
  - [Prim's algorithm](src/Graph/Algorithms/Prim.cs)
  - [Kruskal's algorithm](src/Graph/Algorithms/Kruskal.cs)

- **Cycles**
  - [Detecting cycles in graph](src/Graph/Algorithms/CyclesDetector.cs)
  - [Finding Euler cycle in graph](src/Graph/Algorithms/EulerCycleSearcher.cs)

- **Connected components**
  - [Counting graph's connected components](src/Graph/Algorithms/ConnectedComponentsCounter.cs)
  - [Bridges searching](src/Graph/Algorithms/BridgeSearcher.cs)
  - [Articulation points searching](src/Graph/Algorithms/ArticulationPointSearcher.cs)

- **Other**
  - [Topological sorting](src/Graph/Algorithms/TopologicalSorter.cs)
  - [Graph vertex coloring](src/Graph/Algorithms/VertexColorizer.cs)
