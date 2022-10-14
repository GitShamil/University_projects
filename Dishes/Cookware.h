//
// Created by Шамиль on 25.05.2022.
//

#ifndef DISHES__COOKWARE_H_
#define DISHES__COOKWARE_H_
#include <iostream>
#include <utility>
using namespace std;
class Cookware {
    double price;
    size_t timesOfUsing;
    size_t minutesOfUsing;

 public:
    Cookware();
    Cookware(double Price, size_t TimesOfUsing, size_t MinutesOfUsing);
    void Use(size_t minutes);
    virtual string GetInstruction() const = 0;
    double GetPrice() const;
    size_t GetTimesOfUsing() const;
    size_t GetMinutesOfUsing() const;
    virtual ostream &OutputInformation(ostream &out) const;
};
ostream &operator<<(ostream &out, const Cookware &cookware);

class Plate : public Cookware {
    double volume;
 public:
    Plate();
    Plate(double price, size_t timesOfUsing, size_t minutesOfUsing, double Volume);
    string GetInstruction() const;
    double GetVolume() const;
    ostream &OutputInformation(ostream &out) const override;
};

class Fork : public Cookware {
    short numberOfTeeth;
 public:
    Fork();
    Fork(double price, size_t timesOfUsing, size_t minutesOfUsing, short NumberOfTeeth);
    string GetInstruction() const;
    double GetNumberOfTeeth() const;
    ostream &OutputInformation(ostream &out) const override;
};
#endif //DISHES__COOKWARE_H_
