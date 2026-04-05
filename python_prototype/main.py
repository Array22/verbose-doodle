import json

with open("./map.json", encoding="UTF-8") as f:
    data = json.load(f)

if __name__ == "__main__":
    print(data["single_digit"])
    pass