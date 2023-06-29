package com.lg;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

public class Main {
    public static void main(String[] args){
        System.out.println("JPA project");
        EntityManagerFactory factory = Persistence.createEntityManagerFactory("Hibernate_JPA");
        EntityManager em = factory.createEntityManager();
        em.getTransaction().begin();
        for (Integer i=0;i<5;i++) {
            User u1 = new User(null, "test_"+i, "test_"+i, "Andrzej_"+i, "Kowalski_"+i, Sex.MALE);
            em.persist(u1);
        }
        for (Integer i=0;i<5;i++) {
            Role u1 = new Role(null, "test_"+i);
            em.persist(u1);
        }
        em.getTransaction().commit();
        em.close();
        factory.close();
    }
}
