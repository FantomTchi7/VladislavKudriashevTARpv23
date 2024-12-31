"""Zoo."""

class Animal:
    """Animal class representing an animal in the zoo."""

    def __init__(self, name: str, specie: str, age: int):
        """
        Class constructor.

        Each animal has a name, a species, and an age.
        :param name: animal's name
        :param specie: animal's species
        :param age: animal's age
        """
        self.name = name
        self.specie = specie
        self.age = age

    def __repr__(self):
        return f"Name: {self.name}, specie: {self.specie}, age: {self.age})"


class Zoo:
    """Zoo class representing a zoo with animals."""

    def __init__(self, name: str, max_number_of_animals: int):
        """
        Class constructor.

        Each zoo has a name and a maximum number of animals it can accommodate.
        There is also an overview of all animals present in the zoo.

        :param name: zoo's name
        :param max_number_of_animals: maximum number of animals the zoo can have
        """
        self.name = name
        self.max_number_of_animals = max_number_of_animals
        self.animals = []

    def can_add_animal(self, animal: Animal) -> bool:
        """
        Check if an animal can be added to the zoo.

        An animal can be added to the zoo if:
        1. Adding a new animal does not exceed the zoo's maximum number of animals.
        2. The same Animal object is not already present in the zoo.
        3. An animal with the same name and species is not yet present in the zoo.

        :param animal: animal to be checked
        :return: bool indicating whether the animal can be added to the zoo
        """
        if len(self.animals) < self.max_number_of_animals:
            if animal not in self.animals:
                if not any(a.name == animal.name and a.specie == animal.specie for a in self.animals):
                    return True
        return False

    def add_animal(self, animal: Animal):
        """
        Add an animal to the zoo if possible.

        :param animal: animal to be added to the zoo
        """
        if self.can_add_animal(animal):
            self.animals.append(animal)
        else:
            print(f"Cannot add animal: {animal}")

    def can_remove_animal(self, animal: Animal) -> bool:
        """
        Check if an animal can be removed from the zoo.

        An animal can be removed from the zoo if it is present in the zoo.

        :param animal: animal to be checked
        :return: bool indicating whether the animal can be removed from the zoo
        """
        return animal in self.animals

    def remove_animal(self, animal: Animal):
        """
        Remove an animal from the zoo if possible.

        :param animal: animal to be removed from the zoo
        """
        if self.can_remove_animal(animal):
            self.animals.remove(animal)
        else:
            print(f"Cannot remove animal: {animal}")

    def get_all_animals(self) -> list:
        """
        Return a list of all animals in the zoo.

        :return: list of Animal objects
        """
        return self.animals

    def get_animals_by_age(self) -> list:
        """
        Return a list of animals sorted by age (from younger to older).

        :return: list of Animal objects sorted by age
        """
        return sorted(self.animals, key=lambda animal: animal.age)

    def get_animals_sorted_alphabetically(self) -> list:
        """
        Return a list of animals sorted by name alphabetically.

        :return: list of Animal objects sorted by name alphabetically
        """
        return sorted(self.animals, key=lambda animal: animal.name)
    
if __name__ == "__main__":
    zoo = Zoo("Tallinna Loomaaed", 5)

    animal1 = Animal("Lev", "lion", 5)
    animal2 = Animal("Bogdan", "elephant", 10)
    animal3 = Animal("David", "monkey", 2)
    animal4 = Animal("Timur", "kangaroo", 3)
    animal5 = Animal("Sasha", "zebra", 4)

    zoo.add_animal(animal1)
    zoo.add_animal(animal2)
    zoo.add_animal(animal3)
    zoo.add_animal(animal4)
    zoo.add_animal(animal5)

    animal6 = Animal("Vlad", "giraffe", 6)
    print("Can add Vlad:", zoo.can_add_animal(animal6))

    zoo.remove_animal(animal3)

    print("All animals:", zoo.get_all_animals())

    print("Animals by age:", zoo.get_animals_by_age())

    print("Animals alphabetically:", zoo.get_animals_sorted_alphabetically())