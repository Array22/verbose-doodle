import json

with open("./map.json", encoding="UTF-8") as f:
    data = json.load(f)

#mapping
single_digit = data["ones"]
double_digit = data["double_digit"]
tens = data["tens"]
zeros = data["zeros"]


def convert_cents(x: str):
    """Convert decimal part of a number to words."""
    if len(x) > 2:
        raise ValueError("Please round number to 2 decimal places")
    if len(x) == 1: #e.g. .2
        return f"{tens[x]} CENTS"
    cents_worded = ""
    if x[0] == "0": #e.g. .01
        cents_worded += single_digit[x[1]]
    elif x[0] == "1": #e.g. .12
        cents_worded += double_digit[x]
    
    return f"{cents_worded} CENTS"
    
    

def convert_dollars(x: str):
    """Convert whole part of number to words."""
    pass

def convert_money(x: str):
    """Convert a number to word."""
    try:
        float(x)
    except ValueError:
        print("Please input a numerical number.")
    if "." in x:
        try:
            part_whole, part_int = x.strip().strip("0").split(".")
        except ValueError:
            print(f"{x} is not a valid number")

if __name__ == "__main__":
    # user_input = input("Please input number: ")
    # print(convert_cents(user_input))
    pass