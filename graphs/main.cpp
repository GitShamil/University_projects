#include <iostream>
#include <unordered_map>
#include "defaultgraph.h"
using namespace std;
/// метод вспомогательный для ввода команды
int ChooseCommand() {
    cout << "Choose command:\n" << "1. Add graph\n"<<"2. Output graph\n" <<
         "3. Count amount of vertexes\n" << "4. Count amount of edges\n" <<"5. exit\n";
    int command;
    cin >> command;
    return command;
}

/// точка входа
int main() {
    string command;
    unordered_map<string,Graph> graphs{};
    do {
        int com = ChooseCommand();
        switch (com) {
            case 1:
                AddGraph(graphs);
                break;
            case 2:
                OutputGraph(graphs);
                break;
            case 3:
                CountVertexes(graphs);
                break;
            case 4:
                CountEdges(graphs);
                break;
            case 5:
                return 0;
                break;
        }
    } while (true);
}

