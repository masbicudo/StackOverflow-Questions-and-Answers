


def first_letter_upper(string, exclusions=["a", "the"]):
    words = string.split()
    for i, w in enumerate(words):
        if w not in exclusions:
            words[i] = w[0].upper() + w[1:]
    return " ".join(words)

test = first_letter_upper("miguel angelo santos bicudo")
test2 = first_letter_upper("doing a bunch of things", ["a", "the"])

print(test)
print(test2)