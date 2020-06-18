import matplotlib.pyplot as plt
from matplotlib.ticker import MultipleLocator

x_values = [1,2,3]
y_values = [1,4,9]

plt.plot(x_values,y_values,'g^')
plt.grid(color='black', linestyle="dotted", linewidth=1)
plt.gca().xaxis.set_major_locator(MultipleLocator(1))
plt.gca().yaxis.set_major_locator(MultipleLocator(1))
plt.xlabel("x")
plt.xlim(0.0, 12)
plt.ylabel("y")
plt.ylim(0.0, 12)
plt.show()
