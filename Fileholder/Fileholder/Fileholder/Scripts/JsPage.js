function outer() {
    let x = 5;
    function inner() {
        x++;
        document.write(x + "<br/>");
    };
    return inner;
}
let fn = outer();   // fn = inner, так как функция outer возвращает функцию inner
// вызываем внутреннюю функцию inner
fn();   // 6
fn();   // 7
fn();   // 8

let test = outer();
test();
test();
test();
fn();

