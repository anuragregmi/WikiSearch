#!/usr/bin/python
from sys import *
from urllib2 import *
from json import *
import unicodedata


def find(title):
    # replaces space with %20(character used for space in url)
    title = title.replace(" ", "%20")

    # adding title to request
    url = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles=" + title + "&redirects="

    try:
        # the required data is in {query:{pages{pageid{extract}}}}
        data = urlopen(url)
        jobject = load(data)
        query = jobject['query']
        pages = query['pages']
        for i in pages:
            abc = pages[i]
        topic = abc['title']
        summary = abc['extract']
        summary = seperate(summary)
        result = topic + summary

    except:
        result = "Search did not matched OR could not connect"
    
    return result

def seperate(string):
    array = string.split()

    for i in range(0, len(array), 8):
        array[i] = "\n" + array[i]

    string = " ".join(array)
    print " "
    string = unicodedata.normalize('NFKD', string).encode('ascii', 'ignore') #ignoring non ascii characters

    return string

if __name__ == "__main__":
    if len(argv)>1:
        search = ""
        for i in range(1, len(argv)):
            search = search + " " + argv[i]
        print find(search)
    else :
        print ("Type any thing to Search")



