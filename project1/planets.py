#planets.py
from math import pi

class Planet:

    def __init__(self, name = None, diameter = None, circumference = None, distance = None, orbit = None, moons = None):
        self.name = name
        self.diameter = diameter
        self.circumference = circumference
        self.distance = distance
        self.orbit = orbit
        self.moons = moons
        self.volume = 0

    def setName(self, name):
        self.name = name

    def setDiam(self, diam):
        self.diameter = diam

    def setCirc(self, circ):
        self.circumference = circ

    def setDist(self, dist):
        self.distance = dist
    
    def setOrbit(self, orbit):
        self.orbit = orbit

    def setMoons(self, moons):
        self.moons = moons

    def calcDist(self, orbit):
        self.distance = round((orbit**2)**(1./3.), 2)

    def calcOrbit(self, dist):
        self.orbit = round((dist**3)**.5, 2)
    
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
        
        try:
            self.setDist(data['DistanceFromSun'])
        except:
            self.calcDist(data['OrbitalPeriod'])

        try:
            self.setOrbit(data['OrbitalPeriod'])
        except:
            self.calcOrbit(data['DistanceFromSun'])

        try:
            if(len(data['Moons']) > 0): self.moons = True
        except:
            self.moons = False

        

    def printInfo(self):
        print(f"Name: {self.name}\nDiameter: {self.diameter} km\nCircumference: {self.circumference} km\nDistance from sun: {self.distance} au\nOrbital period: {self.orbit} yr\n")
