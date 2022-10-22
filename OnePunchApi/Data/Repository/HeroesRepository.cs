﻿using Microsoft.EntityFrameworkCore;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data.Repository;

public class HeroesRepository
{
    private readonly AssociationDb _db;

    public HeroesRepository(AssociationDb db)
    {
        _db = db;
    }

    public Hero Create(Hero hero)
    {
        _db.Heroes.Add(hero);
        _db.SaveChanges();
        return hero;
    }

    public IEnumerable<Hero> GetAll()
    {
        return _db.Heroes.ToList();
    }

    public Hero? Get(int id)
    {
        return _db.Heroes.FirstOrDefault(h => h.Id == id);
    }

    public void ChangeRank(Hero hero, Rank rank)
    {
        hero.Rank = rank;
        SaveChanges();
    }

    public void ChangeStatus(Hero hero, Status status)
    {
        hero.Status = status;
        SaveChanges();
    }

    public void SaveChanges()
    {
        _db.SaveChanges();
    }
}