using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : TrackedCell
{
    public override void Step()
    {

        int offsetX = 0;
        int offsetY = 0;

        switch (this.getDirection())
        {
            case (Direction_e.RIGHT):
                offsetX += 1;
                break;
            case (Direction_e.DOWN):
                offsetY += -1;
                break;
            case (Direction_e.LEFT):
                offsetX += -1;
                break;
            case (Direction_e.UP):
                offsetY += 1;
                break;
        }
        //Array index error prevention
        if (this.position.x - offsetX < 0 || this.position.y - offsetY < 0)
            return;
        if (this.position.x - offsetX >= CellFunctions.gridWidth || this.position.y - offsetY >= CellFunctions.gridHeight)
            return;
        if (this.position.x + offsetX < 0 || this.position.y + offsetY < 0)
            return;
        if (this.position.x + offsetX >= CellFunctions.gridWidth || this.position.y + offsetY >= CellFunctions.gridHeight)
            return;

        //If there is a cell in our way, delete it :3
        Cell nextCell = CellFunctions.cellGrid[(int)this.position.x + offsetX, (int)this.position.y + offsetY];
        if (nextCell != null)
        {
            nextCell.Delete(nextCell.generated);
            AudioManager.i.PlaySound(GameAssets.i.destroy);
        }

        this.Push(this.getDirection(), 0);
        //Suppressed will get set to true so we have to reset it.
        this.suppresed = false;
    }

    public override (bool, bool) Push(Direction_e dir, int bias)
    {
        if(this.suppresed)
            return base.Push(dir, bias);
        if (this.getDirection() == dir)
        {
            bias += 1;
        }

        //if bias is opposite our direction
        else if ((int)(dir + 2) % 4 == (int)this.getDirection()) {
            bias -= 1;
        }

        return base.Push(dir, bias);
    }
}
