using System.Collections.Generic;

class Engine
{
    EntityManager em = new DefaultEntityManager();

    public void add(Entity e)
    {
        em.add(e);
    }

    public void remove(Entity e)
    {
        em.remove(e);
    }

    public bool hasEntity(Entity e)
    {
        return em.hasEntity(e);
    }
}

interface EntityManager
{
    void add(Entity e);

    void remove(Entity e);

    bool hasEntity(Entity e);
}

class DefaultEntityManager : EntityManager
{
    List<Entity> entities = new List<Entity>();

    public void add(Entity e)
    {
        entities.Add(e);
    }

    public void remove(Entity e)
    {
        entities.Remove(e);
    }

    public bool hasEntity(Entity e)
    {
        return entities.Contains(e);
    }
}
