﻿using Praktiline_töö_MaduUss;

class ToitLooja
{
    public int mapWidth;
    public int mapHeight;
    public char sym;

    Random random = new Random();

    public ToitLooja(int mapWidth, int mapHeight, char sym)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.sym = sym;
    }

    public Punkt LooToit(List<Punkt> snakeBody)
    {
        Punkt food;
        bool isOnSnake;

        do
        {
            int x = random.Next(2, mapWidth - 2);
            int y = random.Next(2, mapHeight - 2);
            food = new Punkt(x, y, sym);
            isOnSnake = snakeBody.Any(snakePart => snakePart.KasLoob(food));
        }
        while (isOnSnake);

        return food;
    }
}
