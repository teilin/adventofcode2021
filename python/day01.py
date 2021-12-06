import sys
import numpy as np

cmdargs = str(sys.argv)

with open('../data/day01.txt') as f:
    lines = f.readlines()

numbers = list(map(int, lines))

# Part 1

def countIncreasing():
    num = 0
    prevNum = -1
    for i in numbers:
        if prevNum != -1 and prevNum < i:
            num = num+1
        prevNum = i
    return num

print(countIncreasing())

# Part 2

def countIncresingSum():
    array = []
    i = 0
    num = 0
    while i<len(numbers)-2:
       array = np.append(array, numbers[i]+numbers[i+1]+numbers[i+2])
       i = i + 1
    prevNum = -1
    for i in array:
       if prevNum != -1 and prevNum < i:
           num = num+1
       prevNum = i
    return num

print(countIncresingSum())