using UnityEngine;

// Global 데이터를 저장하기 위한 클래스
public static class Global
{
    public readonly static Vector2 flippedScale = new Vector2(-1, 1);

    #region Key Setting
    public readonly static KeyCode KeyJump = KeyCode.Space;
    public readonly static KeyCode KeyLeft = KeyCode.LeftArrow;
    public readonly static KeyCode KeyRight = KeyCode.RightArrow;
    public readonly static KeyCode KeyInteract = KeyCode.LeftControl;
    public readonly static KeyCode KeyCancel = KeyCode.Escape;
    #endregion
}
