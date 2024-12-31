class Product:
    def __init__(self, name, price, id):
        self.name = name
        self.price = price
        self.id = id

    def __eq__(self, other):
        return isinstance(other, Product) and other.id == self.id

class Shop:
    def __init__(self, name):
        self.name = name
        self.products = {}
        self.productCounts = {}
        self.carts = []

    def addProduct(self, product: Product, count: int = 1) -> bool:
        if not product.id:
            return False
        if product.id not in self.products:
            self.products[product.id] = product
            self.productCounts[product.id] = count
        else:
            self.productCounts[product.id] += count
        return True

    def getProductCount(self, product: Product) -> int:
        if product.id not in self.products:
            return -1
        return self.productCounts[product.id]

    def moveToCart(self, product: Product, count=1) -> bool:
        if product.id not in self.products:
            return False
        if count > self.productCounts[product.id]:
            return False
        self.productCounts[product.id] -= count
        return True

class Cart:
    def __init__(self, shop: Shop):
        self.products = {}
        self.shop = shop
    
    def addProduct(self, product: Product, count=1) -> int:
        countInStore = self.shop.getProductCount(product)
        if countInStore <= 0:
            return countInStore
        if countInStore < count:
            count = countInStore
        self.shop.moveToCart(product, count)
        self.products[product.id] = self.products.get(product.id, 0) + count
        return count

    def getTotalPrice(self):
        totalPrice = 0
        for pId, count in self.products.items():
            totalPrice += self.shop.products[pId].price * count
        return totalPrice



if __name__ == "__main__":
    shop1 = Shop("Rama")
    shop1 = Shop("Selma")

    p1 = Product("Milk", 80, 1)
    p1a = Product("Milk", 80, 1)
    p2 = Product("Bread", 120, 2)

shop1.addProduct(p1, 10)
shop1.addProduct(p2, 10)
print(shop1.products)

c1 = Cart(shop1)
print(c1.addProduct(p1, 4))
print(shop1.productCounts)
print(c1.products)
print(c1.addProduct(p1a, 4))
print(shop1.productCounts)
print(c1.products)
c1.addProduct(p2, 3)
print(c1.getTotalPrice())
print(p1 == p1a)