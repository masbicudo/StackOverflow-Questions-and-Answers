#    nums   b    result
# 0  20.0  1    20.0
# 1  22.0  0    0
# 2  30.0  1    0
# 3  29.1  1    0
# 4  20.0  0    0

import pandas as pd
import numpy as np

df = pd.DataFrame({
        "nums": [20, 22, 30, 29.1, 20],
        "b": [20, 22, 30, 29.1, 20],
    })

for i in range(17):
    df = pd.concat([df, df], ignore_index=True)

print(df)

def some_calc_func(prev_result, prev_num, current_b):
    if current_b == 1:
        return prev_result * prev_num / 2
    else:
        return prev_num + 17

result = [20]
for i, row in enumerate(df.itertuples()):
    if i > 0:
        result.append(some_calc_func(result[-1], prev_row[0], row[1]))
    prev_row = row

df["result"] = result
