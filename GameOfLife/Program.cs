int MaxWidth = 80;
int MaxHeight = 25;

char[,] previousFrame = GetFullField();
char[,] currentFrame = GetClearField();

StartLife();

void PrintFrame(char[,] frame)
{
    Console.Clear();

    for (int y = 0; y < MaxHeight; y++, Console.WriteLine())
        for (int x = 0; x < MaxWidth; x++)
            Console.Write(frame[y, x].ToString());
}

char[,] GetClearField()
{
    char[,] field = new char[MaxHeight, MaxWidth];

    for (int y = 0; y < MaxHeight; y++)
        for (int x = 0; x < MaxWidth; x++)
            field[y, x] = ' ';

    return field;
}

char[,] GetFullField()
{
    char[,] field = new char[MaxHeight, MaxWidth];
    Random random = new Random();

    for (int y = 0; y < MaxHeight; y++)
        for (int x = 0; x < MaxWidth; x++)
            field[y, x] = random.Next(0, 10) > 1 ? ' ' : '#';

    return field;
}

void StartLife()
{
    PrintFrame(previousFrame);

    while (true)
    {
        Thread.Sleep(100);

        GetNewFrame();
        PrintFrame(currentFrame);

        previousFrame = currentFrame;
        currentFrame = GetClearField();
    }
}

void GetNewFrame()
{
    for (int y = 0; y < MaxHeight; y++)
        for (int x = 0; x < MaxWidth; x++)
        {
            int neighborsCount = GetNeighborsCount(y, x);

            currentFrame[y, x] = (neighborsCount == 3 && previousFrame[y, x] == ' ') || (neighborsCount == 2 || neighborsCount == 3) && previousFrame[y, x] == '#' ? '#' : ' ';
        }    
}

int GetNeighborsCount(int y, int x)
{
    int neighborsCount = 0;

    for (int cy = y - 1; cy <= y + 1; cy++)
        for (int cx = x - 1; cx <= x + 1; cx++)
        {
            int fy = cy == -1 ? MaxHeight - 1 : cy;
            fy = cy == MaxHeight? 0 : fy;

            int fx = cx == -1 ? MaxWidth - 1 : cx;
            fx = cx == MaxWidth? 0 : fx;

            if (fy == y && fx == x)
                continue;
            else
                if (previousFrame[fy, fx] == '#')
                    neighborsCount++;
        }

    return neighborsCount;
}