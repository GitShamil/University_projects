#pragma once

#include <iostream>
#include <unordered_map>
#include <vector>
using namespace std;
void CommandForDefaultGraph();

/// структура графа
struct Graph {
    bool oriented;
    vector<vector<int>> matrixOfAdjacency;
    vector<vector<int>> matrixOfIncidenty;
    vector<vector<int>> adjacencyList;
    vector<vector<int>> listOfEdges;
    Graph(int typeOfMatrix, bool Oriented, istream& fin, int numberOfLines);
    vector<vector<int>>& GiveNeedMatrix(int number);
    [[nodiscard]] const vector<vector<int>>& GiveNeedMatrix(int number) const;
    void FillOtherMatrix(int typeOfGraph);
    void From1to2();
    void From2to3();
    void From3to4();
    void From4to1();
};

/// Добавить граф
void AddGraph(unordered_map<string,Graph>& graphs);

/// вывод графа
void OutputGraph(const unordered_map<string,Graph>& graphs);

/// подсчет числа вершин
void CountVertexes(const unordered_map<string,Graph>& graphs);

/// подсчет числа ребер
void CountEdges(const unordered_map<string,Graph>& graphs);