data = open("../data/sample-day03.txt", "r").readlines()

# Part 1
gamma = epsilon = ""
for i in range(len(data[0])):
    col = [row[i] for row in data]
    gamma += max(set(col), key=col.count)
    epsilon += min(set(col), key=col.count)

print(int(gamma,2)*int(epsilon,2))

# Part 2
