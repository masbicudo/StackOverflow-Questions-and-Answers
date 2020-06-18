import datetime as dt
from random import random

def rand_date_diff_keep_year_and_interval(dt1, dt2):
    if dt1 > dt2:
        raise Exception("dt1 must be lesser than dt2")
    range1 = {
        "min": dt1.replace(month=1, day=1) - dt1,
        "max": dt1.replace(month=12, day=31) - dt1,
    }
    range2 = {
        "min": dt2.replace(month=1, day=1) - dt2,
        "max": dt2.replace(month=12, day=31) - dt2,
    }
    intersection = {
        "min": max(range1["min"], range2["min"]),
        "max": min(range1["max"], range2["max"]),
    }
    rand_change = random()*(intersection["max"] - intersection["min"]) + intersection["min"]
    return (dt1 + rand_change, dt2 + rand_change)

print(rand_date_diff_keep_year_and_interval(dt.datetime(2000, 1, 1), dt.datetime(2000, 12, 31)))
print(rand_date_diff_keep_year_and_interval(dt.datetime(2000, 5, 18), dt.datetime(2001, 8, 20)))



import datetime as dt
import pandas as pd
import numpy.random as rnd

def series_rand_date_diff_keep_year_and_interval(sdt1, sdt2):
    if any(sdt1 > sdt2):
        raise Exception("dt1 must be lesser than dt2")
    range1 = {
        "min": sdt1.apply(lambda dt1: dt1.replace(month=1, day=1) - dt1),
        "max": sdt1.apply(lambda dt1: dt1.replace(month=12, day=31) - dt1),
    }
    range2 = {
        "min": sdt2.apply(lambda dt2: dt2.replace(month=1, day=1) - dt2),
        "max": sdt2.apply(lambda dt2: dt2.replace(month=12, day=31) - dt2),
    }
    intersection = {
        "min": pd.concat([range1["min"], range2["min"]], axis=1).max(axis=1),
        "max": pd.concat([range1["max"], range2["max"]], axis=1).min(axis=1),
    }
    rand_change = pd.Series(rnd.uniform(size=len(sdt1)))*(intersection["max"] - intersection["min"]) + intersection["min"]
    return (sdt1 + rand_change, sdt2 + rand_change)

df = pd.DataFrame([
        {"start": dt.datetime(2000, 1, 1), "end": dt.datetime(2000, 12, 31)},
        {"start": dt.datetime(2000, 5, 18), "end": dt.datetime(2001, 8, 20)},
    ])

df2 = pd.DataFrame(df)
df2["start"], df2["end"] = series_rand_date_diff_keep_year_and_interval(df["start"], df["end"])
print(df2.head())




import datetime as dt
import pandas as pd
import numpy.random as rnd
import numpy as np
from functools import reduce

def series_rand_date_diff_keep_year_and_interval(*sdts):
    ranges = list(map(
        lambda sdt:
            {
                "min": sdt.apply(lambda dt: dt.replace(month=1,  day=1 ) - dt),
                "max": sdt.apply(lambda dt: dt.replace(month=12, day=31) - dt),
            },
        sdts
        ))
    intersection = reduce(
        lambda range1, range2:
            {
                "min": pd.concat([range1["min"], range2["min"]], axis=1).max(axis=1),
                "max": pd.concat([range1["max"], range2["max"]], axis=1).min(axis=1),
            },
        ranges
        )
    rand_change = pd.Series(rnd.uniform(size=len(intersection["max"])))*(intersection["max"] - intersection["min"]) + intersection["min"]
    return list(map(lambda sdt: sdt + rand_change, sdts))

def setup_diffs(df1, df2):
    df1['diff_birth_death'] = df1['date_death'] - df1['date_birth']
    df1['diff_birth_death'] = df1['diff_birth_death']/np.timedelta64(1,'D')

    df2['diff_birth_diag_start'] = df2['diag_start'] - df1['date_birth']
    df2['diff_birth_diag_end'] = df2['diag_end'] - df1['date_birth']
    df2['diff_birth_diag_start'] = df2['diff_birth_diag_start']/np.timedelta64(1,'D')
    df2['diff_birth_diag_end'] = df2['diff_birth_diag_end']/np.timedelta64(1,'D')

df1 = pd.DataFrame({'person_id': [1, 2, 3, 4, 5],
                        'date_birth': ['12/30/1961', '05/29/1967', '02/03/1957', '7/27/1959', '01/13/1971'],
                        'date_death': ['07/23/2017', '05/29/2017', '02/03/2015', np.nan,      np.nan]})
df1['date_birth'] = pd.to_datetime(df1['date_birth'])
df1['date_death'] = pd.to_datetime(df1['date_death'])

df2 = pd.DataFrame({'person_id': [1,1,1,2,3],
                    'visit_id':['A1','A2','A3','B1','B2'],
                    'diag_start': ['01/01/2012', '02/25/2017', '02/03/2015', '07/27/2016', '01/13/2011'],
                    'diag_end': ['05/03/2012','05/29/2017','03/03/2015','08/15/2016','02/13/2011']})
df2['diag_start'] = pd.to_datetime(df2['diag_start'])
df2['diag_end'] = pd.to_datetime(df2['diag_end'])
setup_diffs(df1, df2)

series_list = series_rand_date_diff_keep_year_and_interval(
    df1['date_birth'], df1['date_death'], df2['diag_start'], df2['diag_end'])
df1['date_birth'], df1['date_death'], df2['diag_start'], df2['diag_end'] = series_list
setup_diffs(df1, df2)

with pd.option_context('display.max_rows', None, 'display.max_columns', None):
    print("")
    print(df1)
    print(df2)
