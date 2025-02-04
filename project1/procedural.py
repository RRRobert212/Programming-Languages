#procedural.py
#procedural approach

import json
from math import pi
#conversion functions for missing
def circToDiam(circ):

    diam = circ/pi

    return round(diam,2)

def diamToCirc(diam):

    circ = pi*diam

    return round(circ, 2)

def orbitToDist(orbit):

    dist = (orbit**2)**(1./3.)

    return round(dist,2)

def distToOrbit(dist):

    orbit = (dist**3)**.5

    return round(orbit,2)

def calcVolume(diam):
    volume = (4/3)*pi*(diam/2)**3
    return volume

def getSunInfo(data):
    sunName = getName(data)
    sunDiam = getDiam(data)
    sunCirc = getCirc(data)

    return sunName, sunDiam, sunCirc

def printSunInfo(data):
    name, diam, circ = getSunInfo(data)
    print(f"\nSun: {name}\nDiameter: {diam}km\nCircumference: {circ}km\n")

def getPlanetInfo(data, planetNum):
    planName = getName(data, planetNum)
    planDist = getDist(data, planetNum)
    planOrbit = getOrbit(data, planetNum)
    planDiam = getDiam(data, planetNum)
    planCirc = getCirc(data, planetNum)
    planMoon = hasMoons(data, planetNum)

    return planName, planDist, planOrbit, planDiam, planCirc, planMoon

#prints when called and also returns True if there are moons, main() takes advantage of this
def printPlanetInfo(planets, planetNum):

    name, dist, orbit, diam, circ, moon = getPlanetInfo(planets, planetNum)
    print(f"............\n\nPlanet: {name}\nDiameter: {diam} km\nCircumference: {circ} km\nDistance From Sun: {dist} au\nOrbital Period: {orbit} yr\n")

    if moon: return True
    else: return False


def getMoonInfo(moons, moonNum):
    moonName = getName(moons, moonNum)
    moonDiam = getDiam(moons, moonNum)
    moonCirc = getCirc(moons, moonNum)

    return moonName, moonDiam, moonCirc

def printMoonInfo(moons, moonNum):
    name, diam, circ = getMoonInfo(moons, moonNum)
    print(f"Moon: {name}\nDiameter: {diam} km\nCircumference: {circ}\n")

def getName(dict, i = None):
    if i == None: return dict['Name']
    else: return dict[i]['Name']

def getDiam(dict, i = None):
    if i == None:
        try: 
            diam = dict['Diameter']
            return diam
        except: 
            diam = circToDiam(dict['Circumference'])
            return diam
    else:
        try: 
            diam = dict[i]['Diameter']
            return diam
        except: 
            diam = circToDiam(dict[i]['Circumference'])
            return diam

def getCirc(dict, i = None):

    if i == None:
        try: 
            circ = dict['Circumference']
            return circ
        except: 
            circ = diamToCirc(dict['Diameter'])
            return circ
    else:
        try: 
            circ = dict[i]['Circumference']
            return circ
        except: 
            circ = diamToCirc(dict[i]['Diameter'])
            return circ
        
def getDist(dict, planetNum):
    try:
        dist = dict[planetNum]['DistanceFromSun']
        return dist
    except:
        dist = orbitToDist(dict[planetNum]['OrbitalPeriod'])
        return dist

def getOrbit(dict, planetNum):
    try:
        orbit = dict[planetNum]['OrbitalPeriod']
        return orbit
    except:
        orbit = distToOrbit(dict[planetNum]['DistanceFromSun'])
        return orbit

#returns true if the planet has moons, by index, 0 being closest to sun and first in planet list, else returns false
def hasMoons(planet, planetnum):
    try:
        planet[planetnum]['Moons']
        return True
    except:
        return False



def main():
    #import data as dictionary
    with open("JSONPlain.txt", "r") as file:
        data = json.load(file)

    planetVolume = 0
    sunVolume = calcVolume(getDiam(data))

    printSunInfo(data)
    planets = data['Planets']


    for i in range(len(planets)):
        planetVolume += calcVolume(getDiam(planets[i]))

        #this is a bit weird but the condition calls printPlanetInfo which prints all info and also returns True if it has moons, in which case moon info gets printed
        if printPlanetInfo(planets, i):
            moons = planets[i]['Moons']
            for j in range(len(moons)):
                printMoonInfo(moons, j)

    if(sunVolume > planetVolume):
        print("Sum of planet's volume is smaller than volume of sun.")
    else:
        print("Sum of planet's volume is greater than or equal to that of the sun.")






if __name__ == '__main__': main()
