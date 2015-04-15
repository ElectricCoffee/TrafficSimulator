#Coding guidelines for C# #

* All names are written in `CamelCase` or `camelCase`, *not* `snake_case`
* Classes all start with a capital letter, i.e. they're `CamelCase`
    - example: `TrafficEventHandler`
* Interfaces also start with a capital letter, and are all prefixed with the letter `I`. Interfaces also usually end on "able"
    - example: `IDrawable`
* Private and local variables
    - start with a lower-case letter, i.e. `camelCase`
    - may be initialised on the spot, or in the constructor, whichever is more convenient
    - example: `private int timeRemaining 40;`
* Public variables must *all* be defined as properties, that is, having a getter and a setter
    - Properties all start with a capital letter, i.e. they're `CamelCase`
    - example: `public int ManaCost { get; set; }`
* Methods, like Properties, must also be capitalised
* Generics start on `T`, if a different generic is used as a return type, use `R`
    - example: `public R Spawner<T, R>(T creature) where T : ICreature`
* All Methods and Properties must be documented using *XML Comments* (see example below)
* Use `//` for commenting a single line, not `/* ... */`
* Use `//` or `/* ... */` for commenting multiple lines, whichever suits you best
    - hint: pressing `Ctrl-k Ctrl-c` comments out the current line, or a selected block of text, `Ctrl-k Ctrl-u` uncommnets.
* When inheriting, put the super-class before the interfaces
    - example: `class Ogre : Creature, IDrawable, IFightable`
* Use `#error` and `#warning` to notify other users about missing features in your code
    - example: `#error CastSpell is currently not fully implemented`

</br></br></br></br></br>

**The following is an example of the aforementioned points, try to keep the code consistent.**

    namespace MyNamespace.MySubNamespace {
        public class MyClass : BaseClass, IInterface1, IInterface2
        {
            // This is a regular comment
            private int myPrivateVar;
            public int MyPublicProperty { get; set; }
            
            /* This is a
               Multi-line
               Comment */
            public MyClass(int a)
            {
                MyPublicProperty = a;
            }
            
            /// <summary>
            /// This is an example of using XML comments for documentation
            /// This is useful for the other members of the group
            /// </summary>
            /// <param name="a">an integer</param>
            /// <param name="b">any type</param>
            public void MyMethod<T>(int a, T b) {
                string myLocalVar = String.Format("{0} {1}", a, b);
                Console.WriteLine(myLocalVar);  
            }
        }
    }