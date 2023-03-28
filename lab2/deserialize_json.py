# -*- coding: utf-8 -*-
"""
deserialize json
"""
import json
class DeserializeJson:
# konstruktor
    def __init__(self, filename):
        print("let's deserialize something")
        tempdata = open(filename, encoding="utf8")
        self.data = json.load(tempdata)
    # przykładowe statystyki
    dictionary={}
    def somestats(self):
        example_stat = 0
        for dep in self.data:

            if not dep['Województwo'].strip() in self.dictionary.keys():
                self.dictionary[dep['Województwo'].strip()]={}
            if not dep['typ_JST'].strip() in self.dictionary[dep['Województwo'].strip()].keys():
                self.dictionary[dep['Województwo'].strip()][dep['typ_JST'].strip()] = 1
            else:
                self.dictionary[dep['Województwo'].strip()][dep['typ_JST'].strip()] += 1

            if dep['typ_JST'] == 'GM' and dep['Województwo'] == 'dolnośląskie':
                example_stat += 1

        for i,j in self.dictionary.items():
            print(i)
            for k,l in j.items():
                print(k+": "+str(l))
            print()
        print()
        print('liczba urzędów miejskich w województwiedolnośląskim: ' + ' ' + str(example_stat))