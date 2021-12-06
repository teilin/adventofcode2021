import sys
import numpy as np

l = []
with open("../data/day02.txt") as f:
    for i in f.readlines():
        tmp = i.split(' ')
        l.append((tmp[0],int(tmp[1])))

# Part 1
x = 0
y = 0
for step in l:
    if step[0] == 'forward':
        x += step[1]
    if step[0] == 'down':
        y += step[1]
    if step[0] == 'up':
        y -= step[1]

print(x*y)

# Part 2
x = 0
y = 0
aim = 0
for step in l:
    if step[0] == 'forward':
        y += (aim*step[1])
        x += step[1]
    if step[0] == 'down':
        aim += step[1]
    if step[0] == 'up':
        aim -= step[1]

print(x*y)