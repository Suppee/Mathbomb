using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolHandler : PoolBase<Symbol> {
    public static PoolHandler Instance; 
    [SerializeField] private Symbol Symbol;
    [SerializeField] private int amount = 48;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else if(Instance != this) {
            Destroy(this);
        }
        for(int i = 0; i < amount; i++) { 
            InitPool(Symbol); // Initialize the pool
        }
    }

    public override Symbol Get(int i) {
        var symbol = base.Get(i);
        if(!symbol.Spawned) symbol.Spawn(i);
        return symbol;
    }
}