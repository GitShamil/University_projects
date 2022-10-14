//
// Created by Шамиль on 25.05.2022.
//

#include "Cookware.h"
void Cookware::Use(size_t minutes) {
    timesOfUsing++;
    minutesOfUsing += minutes;
}
Cookware::Cookware() : price(0), timesOfUsing(0), minutesOfUsing(0) {cout<<"ahah";}
Cookware::Cookware(double Price, size_t TimesOfUsing, size_t MinutesOfUsing)
    : price(Price), timesOfUsing(TimesOfUsing), minutesOfUsing(MinutesOfUsing) {}
double Cookware::GetPrice() const {
    return price;
}
size_t Cookware::GetTimesOfUsing() const {
    return timesOfUsing;
}
size_t Cookware::GetMinutesOfUsing() const {
    return minutesOfUsing;
}
ostream &Cookware::OutputInformation(ostream &out) const {
    out << "it costs: " << this->GetPrice() << "\nit has used: " << this->GetTimesOfUsing() <<
        "\nit has been using for: " << this->GetMinutesOfUsing() << " minutes";
    return out;
}
ostream &operator<<(ostream &out, const Cookware &cookware) {
    return cookware.OutputInformation(out);
}

Plate::Plate() : volume(0), Cookware() {}
Plate::Plate(double price, size_t timesOfUsing, size_t minutesOfUsing, double Volume)
    : volume(Volume), Cookware(price, timesOfUsing, minutesOfUsing) {}
string Plate::GetInstruction() const {
    return "You should use like that: put ready food there and eat\n";
}
double Plate::GetVolume() const {
    return volume;
}
ostream &Plate::OutputInformation(ostream &out) const {
    out << "that's a plate with volume: " << this->GetVolume() << "\n" << this->GetInstruction();
    return Cookware::OutputInformation(out);
}

Fork::Fork() : numberOfTeeth(4), Cookware() {}
Fork::Fork(double price, size_t timesOfUsing, size_t minutesOfUsing, short NumberOfTeeth)
    : numberOfTeeth(NumberOfTeeth), Cookware(price, timesOfUsing, minutesOfUsing) {}
string Fork::GetInstruction() const {
    return "You should use like that: eat solid food with this and you can stick like a pitchfork\n";
}
double Fork::GetNumberOfTeeth() const {
    return numberOfTeeth;
}
ostream &Fork::OutputInformation(ostream &out) const {
    out << "that's a fork with number of teeth: " << this->GetNumberOfTeeth()
        << "\n" << this->GetInstruction();
    return Cookware::OutputInformation(out);
}
