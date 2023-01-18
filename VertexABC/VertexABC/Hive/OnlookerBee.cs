namespace VertexABC.Hive;

class OnlookerBee
{
    public void SetVertexColor(Vertex vertex, List<int> usedColors, Queue<int> availableColors)
    {
        int usedColorIndex = 0;
        while (vertex.IsValid == false || vertex.ColorValue == -1)
        {
            if (usedColorIndex == usedColors.Count - 1 || usedColors.Count == 0)
            {
                int color = availableColors.Dequeue();
                vertex.ColorValue = color;
                usedColors.Add(color);
                return;
            }

            vertex.ColorValue = usedColors[usedColorIndex++];
        }
    }
}