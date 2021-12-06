import sys
import numpy as np

cmdargs = str(sys.argv)

with open('../data/sample-day02.txt') as f:
    lines = f.readlines()

numbers = list(map(int, lines))