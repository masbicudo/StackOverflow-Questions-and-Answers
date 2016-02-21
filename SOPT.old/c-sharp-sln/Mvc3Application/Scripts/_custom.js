
var html = "";

var myFunc = (function (a) {
    return a + arguments[1];
}).WithDefaults([, "y"]);

html += ("myFunc.length => <b>" + myFunc.length + "</b><br>");
html += ("myFunc() => <b>" + myFunc() + "</b><br>");
html += ("myFunc(undefined) => <b>" + myFunc(undefined) + "</b><br>");
html += ("myFunc('') => <b>" + myFunc('') + "</b><br>");
html += ("myFunc('x') => <b>" + myFunc('x') + "</b><br>");
html += ("myFunc(0) => <b>" + myFunc(0) + "</b><br>");
html += ("myFunc(false) => <b>" + myFunc(false) + "</b><br>");
html += ("myFunc(10) => <b>" + myFunc(10) + "</b><br>");
html += ("myFunc(10, 15) => <b>" + myFunc(10, 15) + "</b><br>");
html += ("myFunc.toString() => <b>" + myFunc.toString() + "</b><br>");

var myFunc2 = (function (a, b, c, d) {
    return a + b + c + d;
}).WithDefaults([, "y", 0, "x"]);

html += ("myFunc.length => <b>" + myFunc2.length + "</b><br>");
html += ("myFunc() => <b>" + myFunc2() + "</b><br>");
html += ("myFunc(undefined) => <b>" + myFunc2(undefined) + "</b><br>");
html += ("myFunc('') => <b>" + myFunc2('') + "</b><br>");
html += ("myFunc('x') => <b>" + myFunc2('x') + "</b><br>");
html += ("myFunc(0) => <b>" + myFunc2(0) + "</b><br>");
html += ("myFunc(false) => <b>" + myFunc2(false) + "</b><br>");
html += ("myFunc(10) => <b>" + myFunc2(10) + "</b><br>");
html += ("myFunc(10, 15) => <b>" + myFunc2(10, 15) + "</b><br>");
html += ("myFunc.toString() => <b>" + myFunc2.toString() + "</b><br>");

document.getElementById("exemplos").innerHTML = html;
