using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {
    private static readonly Dictionary<float, WaitForSeconds> waitDict = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time) {
        if(waitDict.TryGetValue(time, out var wait)) return wait;

        waitDict[time] = new WaitForSeconds(time);
        return waitDict[time];
    }

    public static int Prefab2Spawn(char _input) {
        int num = 0;
        switch (_input)
        {
            case '0':
                num = 0;
                break;
            case '1':
                num = 1;
                break;
            case '2':
                num = 2;
                break;
            case '3':
                num = 3;
                break;
            case '4':
                num = 4;
                break;
            case '5':
                num = 5;
                break;
            case '6':
                num = 6;
                break;
            case '7':
                num = 7;
                break;
            case '8':
                num = 8;
                break;
            case '9':
                num = 9;
                break;
            case '*':
                num = 10;
                break;
            case '/':
                num = 11;
                break;
            case '+':
                num = 12;
                break;
            case '-':
                num = 13;
                break;
            case 's':
                num = 14;
                break;
            case 'f':
                num = 15;
                break;
            case '%':
                num = 16;
                break;
            case ',':
                num = 17;
                break;
            case '=':
                num = 18;
                break;
            case 'A':
                num = 19;
                break;
            case 'B':
                num = 20;
                break;
            case 'C':
                num = 21;
                break;
            case 'D':
                num = 22;
                break;
            case 'E':
                num = 23;
                break;
            case 'F':
                num = 24;
                break;
            case 'G':
                num = 25;
                break;
            case 'H':
                num = 26;
                break;
            case 'I':
                num = 27;
                break;
            case 'J':
                num = 28;
                break;
            case 'K':
                num = 29;
                break;
            case 'L':
                num = 30;
                break;
            case 'M':
                num = 31;
                break;
            case 'N':
                num = 32;
                break;
            case 'O':
                num = 33;
                break;
            case 'P':
                num = 34;
                break;
            case 'Q':
                num = 35;
                break;
            case 'R':
                num = 36;
                break;
            case 'S':
                num = 37;
                break;
            case 'T':
                num = 38;
                break;
            case 'U':
                num = 39;
                break;
            case 'V':
                num = 40;
                break;
            case 'W':
                num = 41;
                break;
            case 'X':
                num = 42;
                break;
            case 'Y':
                num = 43;
                break;
            case 'Z':
                num = 44;
                break;
            case '.':
                num = 45;
                break;
            case '\\':
                num = 46;
                break;
            default:
                break;
        }
        return num;
    }

    public static void PrintCharArray(char[] _input) {
        for (int i = 0; i < _input.Length; i++) {
            Debug.Log(_input[i]);
        }
    }

}
