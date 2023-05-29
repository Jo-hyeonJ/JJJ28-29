namespace JJJ28_29
{
    // 공변 타입
    // 상속 관계에서 파생(자식)클래스가 재정의를 할 때
    // 더 하위 타입의 리턴 타입을 정의할 수 있다.
    class Item
    {
        // 부모 : 부모
        public virtual FullStatus GetValue() => null;

    }
    class EquipItem : Item
    {
        Status status;
        // 자식 : 자식
        // 기존에는 동일한 반환형 제한 때문에 FullStatus를 리턴 받아야하여 불편함이 있었다.
        // 이제는 하위 타입으로 반환이 가능하다.
        public override Status GetValue() => null;
        
    }
    class MetarialItem : Item { }
    class FullStatus { }
    class Status : FullStatus { }

    // C# 9.0 - record(aka 읽기 전용)
    // class와 유사하고 struct와 유사한 자료 형태
    // 기본적으로 모든 속성을 Immutable(불변) 상태가 된다.
    // 1. class와 Equals 함수의 동작이 다르다.
    // 2. class와 ToString함수의 동작이 다르다.
    public record Vector(int x, int y);
    // 이렇게 만들어진 자료형은 내부값을 변경 할 수 없다.


    internal class Program
    {

        // class는 동일한 형태이지만 내부 속성의 값이 달라지는 객체를 생성 가능하다.
        // static class는 고유하게 존재하면서 외부에 기능을 제공할 수 있다.
        class Box
        {
            public int a { get; } = 100; // 프로퍼티 초기 값 대입 (읽기 전용 값을 쉽게 설정하는 법)
            public const int A = 100;
            public readonly int B = 100; // readonly : 생성자로 최초 초기화가 가능한 상수화
            public string name;
            public Box(string name)
            {
                this.name = name;
            }
            public void Open()
            {
                Console.WriteLine($"{name}박스를 열었다.");
            }


            public void SetName()
            {

            }
        }
        class B1
        {
            public string name;
            public B1(string name)
            {
                this.name = name;
            }

        }
        record B2(string name);
        static void Main(string[] args)
        {
            /*
                        // null reference Exception 널 참조 예외
                        // = 참조 타입의 변수가 아무것도 참조 하지 않음에도 접근할 때 발생

                        Box box = null;
                        // 변수명? => 널이 아니면 함수 실행
                        box?.SetName();

                        // box가 null을 참조하고 있는 중이라면 대입한다. 
                        box ??= new Box("선물"); // 널 병합 연산자
                        box.Open();

                        // 널 병합 : 널 병합 할당
                        int? a = null;

                        // 1) 널 병합 연산자
                        // => a가 null이 아니라면 a의 값을, null이라면 1을 대입 (좌변의 값이 null일 경우 우변의 값을 리턴.)
                        int b = a ?? 1;

                        // 1-1) 널 병합 할당 연산자
                        // => 좌변에 값이 null일 경우에는 우변의 값(100)을 대입한다.

                        a ??= 100;

                        // 2-1) 예외처리를 하기 위해 자주하는 if연산
                        if(a == null)
                        { a = 100; }

                        // 2-2) 조건 연산자
                        //                      ↓ null을 허용하는 nullable int형이기에 int와 호환되지 않아 형변환을 해줬다.
                        int c = (a != null) ? (int)a: 100;
            */
            if (false)
            {
                // 타겟 타이핑 : new();
                // => 문맥(context)으로부터 어떤 타입인지를 추론한다.
                Dictionary<string, string> closet = new();

                // 자료형을 보면 알아서 대입해준 것을 확인할 수 있다.
                var closet2 = closet;

                // 직접 만든 클래스에도 정상적으로 작동한다. 좌변의 자료형을 보고 우변의 자료형 유추
                Box box2 = new("asd");

                // 타겟 타이핑 : 조건 연산자(?)
                // => 공유되는 타입이 있다면 조건 연산자에서 사용이 가능하다.
                // 본래 같은 타입의 자료형만 조건 연산자 양변에 사용이 가능했다.

                int index = 0;
                Item item = (index == 0) ? new EquipItem() : new MetarialItem();
                int? num1 = (index == 0) ? 0 : null;

                // 논리 연산자 활용
                if (item is EquipItem equip)
                    Console.WriteLine("장비아이템");
                if (item is not EquipItem equip2)
                    Console.WriteLine("장비 아님");

                // 패턴 매칭
                int a = 100;
                string b = a switch
                {
                    10 => "A",
                    20 => "B",
                    _ => "" // 취소 연산자도 반드시 포함되어야했었다.
                };
            }

            Vector v = new Vector(10, 20);
            Vector v2 = new Vector(10, 20);
            Box giftBox = new("선물");
            Box giftBox2 = new("선물");

            // Record의 ToString형태
            Console.WriteLine(v); // = Vector { x = 10, y = 20 }
            Console.WriteLine(giftBox); // JJJ28_29.Program + Box

            // 기존 class는 참조 값을 비교하는 형태이기에 값이 같을 수 없다.
            Console.WriteLine(giftBox.Equals(giftBox2));    // false
            // Record 자료형은 불변이기에 참조 변수로도 값의 비교가 가능하다.
            Console.WriteLine(v.Equals(v2));                // true

            B2 b2 = new("as");
            B1 b1 = new("as");
            B2 b3 = GetB2("as");

            Console.WriteLine (b2.Equals(b1)); 
            Console.WriteLine (b2.Equals(b3)); 

            // 구조체 : 값 형식의 개체
            // 클래스 : 참조 형식의 객체
            // 레코드 : 기본적으로 불변 형식의 객체 (참조 타입)

            // 무명 형식 : 클래스의 리터널 버전 (클래스 타입)
            // 튜플 형식 : 복합 자료형 (자료 타입)
        }
        static string GetRank(int score)
        {
            /* 본래 형태
            string rank = score switch
            {
                
                var num when (num >= 90) => "A",

            };
            */

            // 향상된 패턴 매칭 : 관계 연산자 패턴
            string rank = score switch
            {
                >= 90 => "A",
                >= 80 => "B"
            };

            // 향상된 패턴 매칭 : 논리 패턴
            rank = score switch
            {
               >= 90 and <= 100 => "a",     // and &&
               > 1 or < 1 => "Z",           // or ||
               not -100 => "T",             // not !
            };

            return rank;
        }
        private void Func()
        {
            int a = 10;

            // 로컬 함수
            void FuncLocal()
            {
                a = 200;
            }

            // static 로컬 함수 (안전장치)
            static void FuncStatic()
            {
                // 해당 함수는 static 필드에 있기 때문에 local 필드의 값에 접근이 불가능
                // 무분별하게 Func함수 내의 값에 접근해 값을 잘못 변경 할 수도 있기 때문에 사용
                // a = 200;
            }
            

        }
        static B2 GetB2(string name)
        {
            return new B2(name);
        }
    }

}