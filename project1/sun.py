#sun.py

from math import pi

class Sun:

    def __init__(self, name = None, diameter = None, circumference = None, planets = None):
        self.name = name
        self.diameter = diameter
        self.circumference = circumference
        self.planets = planets

    def setName(self, name):
        self.name = name

    def setDiam(self, diam):
        self.diameter = diam

    def setCirc(self, circ):
        self.circumference = circ

    def setPlanets(self, plans):
        self.planets = plans
    
    def calcDiam(self, circ):
        self.diameter = round(circ/pi,2)

    def calcCirc(self, diam):
        self.circumference = round(pi*diam,2)

    def calcVolume(self):
        self.volume = round((4/3)*pi*((self.diameter/2)**3), 2)

    def setAllInfo(self, data):
        self.setName(data['Name'])
        try:
            self.setCirc(data['Circumference'])
        except:
            self.calcCirc(data['Diameter'])
        try:
            self.setDiam(data['Diameter'])
        except:
            self.calcDiam(data['Circumference'])

    def printInfo(self):
        print(f"Name: {self.name}\nDiameter: {self.diameter} km\nCircumference: {self.circumference} km\n")
