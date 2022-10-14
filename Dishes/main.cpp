#include <iostream>
#include <vector>
#include <memory>
#include "Cookware.h"
using namespace std;
int main() {
   vector<shared_ptr<Cookware>> cookwares;
   cookwares.push_back(shared_ptr<Cookware>(new Fork{}));
   cookwares.push_back(shared_ptr<Cookware>(nullptr));
   cookwares.push_back(shared_ptr<Cookware>(new Plate{50,2,3,100}));
   cookwares.push_back(shared_ptr<Cookware>(new Fork{10,50,150,5}));
   for(auto el:cookwares){
       cout<<*el<<"\n\n";
       el->Use(rand());
       cout<<*el<<"\n\n";
   }
}
