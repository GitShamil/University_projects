//
// Created by Шамиль on 10.05.2022.
//
#include <iostream>
#include <sstream>
#include <fstream>
#include "defaultgraph.h"
using namespace std;

/// Ввод матрицы из потока
/// \param fin поток
/// \param numberOfLines число строк
/// \param matrix  матрица
void InputMatrix(istream &fin, int numberOfLines, vector<vector<int>> &matrix) {
    for (int i = 0; i < numberOfLines; ++i) {
        string line;
        getline(fin >> ws, line);
        stringstream str(line);
        matrix.emplace_back(vector<int>{});
        int number;
        while (str >> number) {
            matrix[i].push_back(number);
        }
    }
}

///  вывод матрицы в поток
/// \param out поток
/// \param matrix  матрица
void OutputMatrix(ostream &out, const vector<vector<int>> &matrix) {
    for (const auto &el1 : matrix) {
        for (auto el2 : el1) {
            out << el2 << "\t";
        }
        out << endl;
    }
}

/// Добавить граф в множество графов
/// \param graphs  графы
void AddGraph(unordered_map<string, Graph> &graphs) {
    cout << "Choose command:\n" << "1. Add simple graph\n" << "2. Add oriented graph\n";
    int command;
    cin >> command;
    bool oriented = command - 1;
    cout << "Input name for graph: ";
    string name;
    cin >> name;
    cout << "Choose type of matrix:\n" << "1. matrix of adjacency\n" << "2. matrix of incidenty\n" <<
         "3. Adjacency list\n" << "4. List of edges\n";
    int typeOfMatrix;
    cin >> typeOfMatrix;
    cout << "Input how many lines in input of graph: ";
    int numberOfLines;
    cin >> numberOfLines;
    cout << "Choose type of Input:\n" << "1. console\n" << "2. file\n";
    cin >> command;
    if (command == 1) {
        graphs.try_emplace(name, typeOfMatrix, oriented, cin, numberOfLines);
    } else {
        cout << "input relative name of file: ";
        string nameOfFile;
        cin >> nameOfFile;
        fstream fin("../" + nameOfFile);
        graphs.try_emplace(name, typeOfMatrix, oriented, fin, numberOfLines);
    }
}

/// вывод графа
/// \param graphs  графы
void OutputGraph(const unordered_map<string, Graph> &graphs) {
    cout << "Input name for graph: ";
    string name;
    cin >> name;
    int command;
    cout << "Choose type of Output:\n" << "1. console\n" << "2. file\n";
    cin >> command;
    cout << "Choose type of matrix:\n" << "1. matrix of adjacency\n" << "2. matrix of incidenty\n" <<
         "3. Adjacency list\n" << "4. List of edges\n";
    int typeOfMatrix;
    cin >> typeOfMatrix;
    const Graph &graph = graphs.at(name);
    if (command == 1) {
        OutputMatrix(cout, graph.GiveNeedMatrix(typeOfMatrix));
    } else {
        cout << "input relative name of file: ";
        string nameOfFile;
        cin >> nameOfFile;
        fstream fin("../" + nameOfFile,ios::out);
        if (fin)
        OutputMatrix(fin, graph.GiveNeedMatrix(typeOfMatrix));
    }
}

/// Подсчет числа вершин
/// \param graphs
void CountVertexes(const unordered_map<string, Graph> &graphs) {
    cout << "Input name for graph: ";
    string name;
    cin >> name;
    const Graph &graph = graphs.at(name);
    cout << "number of vertexes: " << graph.matrixOfIncidenty.size() << "\n";
}

/// Подсчет ребер
/// \param graphs
void CountEdges(const unordered_map<string, Graph> &graphs) {
    cout << "Input name for graph: ";
    string name;
    cin >> name;
    const Graph &graph = graphs.at(name);
    cout << "number of edges: " << graph.matrixOfIncidenty[0].size() << "\n";
}

/// конструктор графа
/// \param typeOfMatrix  тип матрицы которая дана в конструктор
/// \param Oriented  ориентированный или нет
/// \param fin  поток ввода
/// \param numberOfLines  число строк
Graph::Graph(int typeOfMatrix, bool Oriented, istream &fin, int numberOfLines) {
    oriented = Oriented;
    auto &mat = GiveNeedMatrix(typeOfMatrix);
    InputMatrix(fin, numberOfLines, mat);
    FillOtherMatrix(typeOfMatrix);
}

/// вспомогательный метод дающий нужную матрицу по номер типа матрицы
/// \param number  номер типа матрицы
/// \return
vector<vector<int>> &Graph::GiveNeedMatrix(int number) {
    switch (number) {
        case 1:return matrixOfAdjacency;
        case 2:return matrixOfIncidenty;
        case 3:return adjacencyList;
        case 4:return listOfEdges;
    }
}

/// вспомогательный метод дающий нужную матрицу по номер типа матрицы
/// \param number  номер типа матрицы
/// \return
const vector<vector<int>> &Graph::GiveNeedMatrix(int number) const {
    switch (number) {
        case 1:return matrixOfAdjacency;
        case 2:return matrixOfIncidenty;
        case 3:return adjacencyList;
        case 4:return listOfEdges;
    }
}

/// Заполнение всех видов матрицы
void Graph::FillOtherMatrix(int typeOfMatrix) {
    switch (typeOfMatrix) {
        case 1:From1to2();
            From2to3();
            From3to4();
            break;
        case 2:From2to3();
            From3to4();
            From4to1();
            break;
        case 3:From3to4();
            From4to1();
            From1to2();
            break;
        case 4:From4to1();
            From1to2();
            From2to3();
            break;
    }
}

/// Переход от 1-го типа ко 2-му
void Graph::From1to2() {
    for (size_t i = 0; i < matrixOfAdjacency.size(); ++i) {
        matrixOfIncidenty.emplace_back(vector<int>{});
    }
    if (!oriented) {
        for (size_t i = 0; i < matrixOfAdjacency.size(); ++i) {
            for (size_t j = i; j < matrixOfAdjacency.size(); ++j) {
                if (matrixOfAdjacency[i][j] == 1) {
                    for (size_t k = 0; k < matrixOfIncidenty.size(); ++k) {
                        if (k == i || k == j)
                            matrixOfIncidenty[k].push_back(1);
                        else
                            matrixOfIncidenty[k].push_back(0);
                    }
                }
            }
        }
    } else {
        for (size_t i = 0; i < matrixOfAdjacency.size(); ++i) {
            for (size_t j = 0; j < matrixOfAdjacency.size(); ++j) {
                if (matrixOfAdjacency[i][j] == 1) {
                    if (matrixOfAdjacency[j][i] != 1) {
                        for (size_t k = 0; k < matrixOfIncidenty.size(); ++k) {
                            if (k == i)
                                matrixOfIncidenty[k].push_back(1);
                            if (k == j)
                                matrixOfIncidenty[k].push_back(-1);
                            if (k != i && k != j)
                                matrixOfIncidenty[k].push_back(0);
                        }
                    } else {
                        if (i > j) {
                            for (size_t k = 0; k < matrixOfIncidenty.size(); ++k) {
                                if (k == i || k == j)
                                    matrixOfIncidenty[k].push_back(1);
                                else
                                    matrixOfIncidenty[k].push_back(0);
                            }
                        }
                    }
                }

            }
        }
    }
}

/// Переход от 2-го типа ко 3-му
void Graph::From2to3() {
    if (!oriented) {
        for (size_t i = 0; i < matrixOfIncidenty.size(); ++i) {
            adjacencyList.emplace_back(vector<int>{});
            for (size_t j = 0; j < matrixOfIncidenty[0].size(); ++j) {
                if (matrixOfIncidenty[i][j] == 1) {
                    for (size_t k = 0; k < matrixOfIncidenty.size(); ++k) {
                        if (matrixOfIncidenty[k][j] == 1 && k != i)
                            adjacencyList[i].push_back(k);
                    }
                }
            }
        }
    } else {
        for (size_t i = 0; i < matrixOfIncidenty.size(); ++i) {
            adjacencyList.emplace_back(vector<int>{});
            for (size_t j = 0; j < matrixOfIncidenty[0].size(); ++j) {
                if (matrixOfIncidenty[i][j] == 1) {
                    for (size_t k = 0; k < matrixOfIncidenty.size(); ++k) {
                        if ((matrixOfIncidenty[k][j] == 1 || matrixOfIncidenty[k][j] == -1) && k != i)
                            adjacencyList[i].push_back(k);
                    }
                }
            }
        }
    }
}

/// Переход от 3-го типа ко 4-му
void Graph::From3to4() {
    if (!oriented){
        for (size_t i = 0; i < adjacencyList.size(); ++i) {
            for (size_t j = 0; j < adjacencyList[i].size(); ++j) {
                int from = i;
                int to = adjacencyList[i][j];
                bool well = true;
                for(auto el: listOfEdges){
                    if (el[0]==to && el[1]==from)
                        well =false;
                }
                if (well){
                    listOfEdges.emplace_back(vector<int>{from,to});
                }
            }
        }
    }else{
        for (size_t i = 0; i < adjacencyList.size(); ++i) {
            for (size_t j = 0; j < adjacencyList[i].size(); ++j) {
                int from = i;
                int to = adjacencyList[i][j];
                    listOfEdges.emplace_back(vector<int>{from,to});
            }
        }
    }
}
/// Переход от 4-го типа ко 1-му
void Graph::From4to1() {
    int numberOfVertexes = 0;
    for (size_t i = 0; i < listOfEdges.size(); ++i) {
        for (size_t j = 0; j < listOfEdges[i].size(); ++j) {
            numberOfVertexes=max(numberOfVertexes,listOfEdges[i][j]);
        }
    }
    numberOfVertexes++;
    if (!oriented){
        for (int i = 0; i < numberOfVertexes; ++i) {
            matrixOfAdjacency.emplace_back(vector<int>(numberOfVertexes));
        }
        for (size_t i = 0; i < listOfEdges.size(); ++i) {
            matrixOfAdjacency[listOfEdges[i][0]][listOfEdges[i][1]]=1;
            matrixOfAdjacency[listOfEdges[i][1]][listOfEdges[i][0]]=1;
        }
    }else{
        for (int i = 0; i < numberOfVertexes; ++i) {
            matrixOfAdjacency.emplace_back(vector<int>(numberOfVertexes));
        }
        for (size_t i = 0; i < listOfEdges.size(); ++i) {
            matrixOfAdjacency[listOfEdges[i][0]][listOfEdges[i][1]]=1;
        }
    }
}
