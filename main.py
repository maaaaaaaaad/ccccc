# import pandas as pd
#
# df = pd.read_csv('./datas/namsan_library_popular_books_2023_05.csv', encoding='EUC_KR', low_memory=False)
# df.drop('Unnamed: 13', axis=1, inplace=True)
# df.dropna(axis=1, how='all', inplace=True)
# print(df.head())
#
# selected_rows = df['출판사'] == '우리문학사'
# result = df[selected_rows]
# print(result)
import random

x = random.randrange(0, 5)

success = 0
fail = 0

for i in range(10000):
    while True:
        if x == 0:
            print('success')
            break
        else:
            print('fail')

result = (success / (success + fail))

print(result)
