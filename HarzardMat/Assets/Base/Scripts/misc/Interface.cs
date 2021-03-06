﻿using System.Collections;
using System.Collections.Generic;


public interface ITakeDamage<T>
{
    void Damage(T damage);
}

public interface IDie
{
    void Die();
}

public interface ISpawnerID<T>
{
    void SetSpawnerID(T newID);
}