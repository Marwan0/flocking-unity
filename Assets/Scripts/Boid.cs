using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The Boid class
// adapted from https://processing.org/examples/flocking.html



public class Boid {

    public Vector2 location;
    public Vector2 velocity;
    Vector2 acceleration;
    float size; // size of boid
    float maxforce;    // Maximum steering force
    float maxspeed;    // Maximum speed

    public Boid(float x, float y) {
        acceleration = new Vector2(0, 0);

        // This is a new Vector2 method not yet implemented in JS
        // velocity = Vector2.random2D();

        // Leaving the code temporarily this way so that this example runs in JS
        float angle = Random.Range(0,Mathf.PI*2);
        velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        location = new Vector2(x,y);
        size = 10;
        maxspeed = 2f;
        maxforce = 0.1f;
    }

    public void Update(List<Boid> boids) {
        Flock(boids);
        UpdateLocation();
        Borders();
    }

    void ApplyForce(Vector2 force) {
        // We could add mass here if we want A = F / M
        acceleration += force;
    }

    // We accumulate a new acceleration each time based on three rules
    void Flock(List<Boid> boids) {
        Vector2 sep = Separate(boids);   // Separation
        Vector2 ali = Align(boids);      // Alignment
        Vector2 coh = Cohesion(boids);   // Cohesion
        // Arbitrarily weight these forces
        sep *= 2.5f;
        ali *= 1;
        coh *= 1;
        // Add the force vectors to acceleration
        ApplyForce(sep);
        ApplyForce(ali);
        ApplyForce(coh);
    }

    // Method to update location
    void UpdateLocation() {
        // Update velocity
        velocity += acceleration;
        // Limit speed
        velocity = Limit(velocity, maxspeed);
        location += velocity;
        // Reset accelertion to 0 each cycle
        acceleration *= 0;
    }

    // A method that calculates and applies a steering force towards a target
    // STEER = DESIRED MINUS VELOCITY
    Vector2 Seek(Vector2 target) {
        Vector2 desired = target -location;  // A vector pointing from the location to the target
        // Scale to maximum speed
        desired.Normalize();
        desired *= maxspeed;

        // Above two lines of code below could be condensed with new Vector2 setMag() method
        // Not using this method until Processing.js catches up
        // desired.setMag(maxspeed);

        // Steering = Desired minus Velocity
        Vector2 steer = desired - velocity;
        steer = Limit(steer,maxforce);  // Limit to maximum steering force
        return steer;
    }

    // Wraparound
    void Borders() {
        if (location.x < -size) location.x = Main.viewSize+size;
        if (location.y < -size) location.y = Main.viewSize+size;
        if (location.x > Main.viewSize+size) location.x = -size;
        if (location.y > Main.viewSize+size) location.y = -size;
    }

    // Separation
    // Method checks for nearby boids and steers away
    Vector2 Separate (List<Boid> boids) {
        float desiredseparation = size;
        Vector2 steer = new Vector2(0, 0);
        int count = 0;
        // For every boid in the system, check if it's too close
        foreach (var other in boids) {
            float d = Vector2.Distance(location, other.location);
            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself)
            if ((d > 0) && (d < desiredseparation)) {
                // Calculate vector pointing away from neighbor
                Vector2 diff = location - other.location;
                diff.Normalize();
//                diff.Scale(new Vector2(d,d));        // Weight by distance
                diff *= d;        // Weight by distance
                steer += diff;
                count++;            // Keep track of how many
            }
        }
        // Average -- divide by how many
        if (count > 0) {
            steer /= (float)count;
        }

        // As long as the vector is greater than 0
        if (steer.sqrMagnitude > 0) {
            // First two lines of code below could be condensed with new Vector2 setMag() method
            // Not using this method until Processing.js catches up
            // steer.setMag(maxspeed);

            // Implement Reynolds: Steering = Desired - Velocity
            steer.Normalize();
            steer *= maxspeed;
            steer -= velocity;
            steer = Limit(steer, maxforce);
        }
        return steer;
    }

    // Alignment
    // For every nearby boid in the system, calculate the average velocity
    Vector2 Align (List<Boid> boids) {
        float neighbordist = 50;
        Vector2 sum = new Vector2(0, 0);
        int count = 0;
        foreach (var other in boids) {
            float d = Vector2.Distance(location, other.location);
            if ((d > 0) && (d < neighbordist)) {
                sum += other.velocity;
                count++;
            }
        }
        if (count > 0) {
            sum /= (float)count;
            // First two lines of code below could be condensed with new Vector2 setMag() method
            // Not using this method until Processing.js catches up
            // sum.setMag(maxspeed);

            // Implement Reynolds: Steering = Desired - Velocity
            sum.Normalize();
            sum *= maxspeed;
            Vector2 steer = sum - velocity;
            steer = Limit(steer,maxforce);
            return steer;
        } 
        else {
            return new Vector2(0, 0);
        }
    }

    // Cohesion
    // For the average location (i.e. center) of all nearby boids, calculate steering vector towards that location
    Vector2 Cohesion (List<Boid> boids) {
        float neighbordist = 50;
        Vector2 sum = new Vector2(0, 0);   // Start with empty vector to accumulate all locations
        int count = 0;
        foreach (var other in boids) {
            float d = Vector2.Distance(location, other.location);
            if ((d > 0) && (d < neighbordist)) {
                sum += other.location; // Add location
                count++;
            }
        }
        if (count > 0) {
            sum /= count;
            return Seek(sum);  // Steer towards the location
        } 
        else {
            return new Vector2(0, 0);
        }
    }

    Vector2 Limit ( Vector2 vector, float max ) {
        if ( vector.sqrMagnitude > max*max )
            return vector.normalized*max;
        else
            return vector;
    }
}
