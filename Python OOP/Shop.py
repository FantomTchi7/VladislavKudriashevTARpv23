class Product:
    def __init__(self, name, price):
        self.name = name
        self.price = price

class Shop:
    def __init__(self, name):
        self.name = name
        self.products = []
        self.carts = []

    def addProduct(self, product: Product, count: int = 1) -> bool:
        self.products.append(product)

class Cart:
    def __init__(self):
        self.products = []

if __name__ == "__main__":
    shop1 = Shop("Rama")
    shop1 = Shop("Selma")

    p1 = Product("Milk", 80)
    p2 = Product("Bread", 120)