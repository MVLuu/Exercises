import sys

infile, searchString = sys.argv[1], sys.argv[2]
datafile = open(infile)

def check():
    found = False    
    for line in datafile:
        
        if searchString in line:
            utf8Encode = line.encode("utf8","ignore")
            print(str(utf8Encode)[2:-3])
            found = True
        
    if found == False:
            print("String not found")
check()
