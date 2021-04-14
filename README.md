# flightSim

1.	תיעוד והסבר קצר על הפרויקט ועל פיצ'רים מיוחדים שמימשנו בפרויקט

 בנינו אפליקצית desktop של סימולטור טיסה. 
האפליקציה מקבלת קובץ csv עם פרטים על הטיסה, ומציגה סרטון של הטיסה בעזרת תוכנת  flight gear.  
לפי הדרישות שקיבלנו מחוקרי הטיסה בנינו נגן שמאפשר לעבור מכל שנייה בסרטון לכל שנייה אחרת שנרצה. נוכל להריץ את הסרטון במהירות שונה, לפי כל מספר שנגדיר, כך שניתן להתמקד בקטעי טיסה מסוימים ולראות בריפרוף קטעי טיסה אחרים. 
כמו כן ייצרנו ג'ויסטיק שמציג את מצב ההגאים הראשיים של המטוס והצגנו נתונים רלוונטיים נוספים. במהלך הצפיה בטיסה המשתמש יכול לבחור נתון מסוים ולחקור אותו: לקבל גרף של הנתון שמתעדכן עם הרצת סרטון הטיסה, לראות מי מכל שאר הערכים הכי קורלטיבי אליו ולראות את קו הרגרסיה הלינארית ביניהם.
פיצ'רים מיוחדים שמימשנו: השתמש בתבנית העיצוב Singleton עבור המחלקה program. מחלקת הprogram היא המחלקה האחראית על קריאת קובץ הCSV אל תוך מבנה נתונים, והוצאת הנתונים ממנו לפי הצורך.
כל מחלקה אחרת שרוצה לקבל נתונים מסוימים בנוגע לטיסה משתמשת במתודות של program על מנת לקבל אותם. כיוון שכל הרצה של הפרויקט נדרשת לקרוא קובץ CSV אחד בלבד, אין צורך ליותר ממופע אחד של מחלקת הprogram. לכן החלטנו להשתמש בתבנית העיצוב singleton שלא מאפשרת יצירה של יותר ממופע אחד של המחלקה. כך כל מחלקה יכולה לפנות בקלות למחלקה program ולקבל נתונים שונים אודות הטיסה מבלי שיווצרו מופעים רבים של program.

2.	תיעוד והסבר כללי על מבנה התיקיות והקבצים הראשיים בפרויקט

DataBase: קורא את הCSV אל תוך מבנה נתונים. לאחר מכן יהיה ניתן לגשת למבנה הנתונים בעזרת Program ולקבל ערכים שונים של מאפיינים בנקודות זמן מסוימות. DataBase משתמשת במחלקה Setting על מנת לקרוא את הקובץ XML, ולפיו לדעת מה יהיו שמות העמודות במבנה הנתונים.
Program: מקבל את מבנה הנתונים מהDataBase.
יוצר חיבור TCP מול התוכנה flight gear ושולח לו כל פעם את השורה שהוא רוצה להראות בסימולטור. אם המשתמש לא מתערב התוכנית תשלח את השורות בקצב של 10 שורות לשנייה.
 הBinding לכל הפרמטרים שמשפיעים על שליחת השורות נעשה מולו, ויש לו שדות ומתודות שמאפשרות שינויים על ידי המשתמש.
כשprogram מתחיל לרוץ הוא חייב שקובצי הCSV והXML יהיו במקום שלהם, אחרת תיזרק שגיאה.
MainWindow: החלון הראשון שנפתח, אומר למשתמש איפה לשים את קובץ הCSV והXML. כשהמשתמש לוחץ done נפתח לו Window2, בחלון זה נמצא הנגן, הג'ויסטיק, תצוגת הפרמטרים של סיפור משתמש 5 ותצוגת הגרפים השונים.
לכל userControl שמופיע במסך הראשי יש 3 קבצים:
•	קובץ xaml: גרפיקת הפקד
•	cs: code behind 
•	VM.cs: הView Model , קובץ בו נמצא הקוד שיעשה את החיבור בין הView שנמצא בקובץ הxaml לבין הModel שנמצא בProgram.


3.	

הוראות התקנה וריצה ראשונית של האפליקציה שלנו על מחשב חדש:
1) על מנת להריץ את התוכנית על המשתמש להוריד את ??? ו
2) להוריד את התוכנה FlightGear 2020.3.6 על מנת שהסרטון יתנגן. בתיקייה של הFlightGear צריך להכניס את הקובץ XML בנתיב /data/protocol.
לפני ההרצה הראשונית על המשתמש להיכנס לFlightGear, בתפריט מצד שמאל ללחוץ על settings  ובחלונית שמופיעה למטה להכניס את השורות הבאות:
--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null

קישור לתמונה 3
כמו כן צריך להכניס את הקובץ CSV והקובץ XML בשמות ובנתיב שמופיע בחלון שעולה 
לאחר הרצת התוכנית.

4.

כשהמשתמש ירצה להשתמש בתוכנה, הוא יצטרך קודם כל להיכנס לFlight Gear וללחוץ על Fly.
כעת הוא יכול להריץ את האפליקציה שלנו. האפליקציה שלנו תגיד לו היכן לשים את קובץ הCSV של נתוני הטיסה ואיך לקרוא לו, ואותו הדבר גם לגבי קובץ הXML. לאחר מכן הוא ילחץ Done ויופיע לו החלון הבא:

קישור לתמונה 4
כעת עליו ללחוץ play בנגן.
5. 

קישור לקבצי תיעוד נוספים בתוך הgit המכילים תיעוד מפורט של המחלקות הראשיות בפרויקט, זרמית מידע, תרשמי UML של המחלקות הראשיות. דגש על MVVM
קישור לתמונה 5
6.
קישור לסרטון עד 6 דקות של הדגמת סיפורי המשתמש
