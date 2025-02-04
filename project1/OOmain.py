#Object-Oriented-main.py

import json
from sun import Sun
from planets import Planet
from moons import Moon

def main():

    #import data as dictionary
    with open("JSONPlain.txt", "r") as file:
        data = json.load(file)

    planetVolume = 0
    sunVolume = 0
        
    sun = Sun()
    sun.setAllInfo(data)
    sun.calcVolume()
    sunVolume = sun.volume
    print("--------------------")
    print("Sun:\n")
    sun.printInfo()
    print("--------------------")
    print("Planets:\n")

    planetsList = data['Planets']

    for i in range(len(planetsList)):
        planet = Planet()
        planet.setAllInfo(planetsList[i])
        planet.calcVolume()
        planetVolume += planet.volume
        planet.printInfo()
        if planet.moons:
            print("Moons:\n")
            moonList = planetsList[i]['Moons']
            for i in range(len(moonList)):
                moon = Moon()
                moon.setAllInfo(moonList[i])
                moon.printInfo()
        print("--------------------")

    if(sunVolume > planetVolume):
        print("Sum of planet's volume is smaller than volume of sun.")
    else:
        print("Sum of planet's volume is greater than or equal to that of the sun.")


    return




if __name__ == "__main__": main()