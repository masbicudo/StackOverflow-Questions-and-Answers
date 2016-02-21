(function () {
    var setDefaults = function (a, d) {
        var l = Math.max(a.length, d.length);
        var p = [];
        for (var i = 0; i < l; i++)
            p[i] = i >= a.length || typeof a[i] == 'undefined' ? d[i] : a[i];
        return p;
    }

    var copyProperties = function (to, from, defs) {
        to.innerFunction = from;
        to.toString = function () {
            var strDefs = "";
            for (var i = 0; i < defs.length; i++)
                strDefs += (i > 0 ? ", " : "") + (typeof defs[i] != 'undefined' ? JSON.stringify(defs[i]) : "");

            return "(" + from.toString() + ").WithDefaults(["
                + strDefs + "])";
        };
        for (var key in from)
            to[key] = from[key];
        return to;
    }

    var fnCreators = {
        0: function (f, d, sd, cp) {
            return cp(function () {
                return f.apply(this, sd(arguments, d));
            }, f, d);
        },
        1: function (f, d, sd, cp) {
            return cp(function (p1) {
                return f.apply(this, sd(arguments, d));
            }, f, d);
        },
        2: function (f, d, sd, cp) {
            return cp(function (p1, p2) {
                return f.apply(this, sd(arguments, d));
            }, f, d);
        },
        3: function (f, d, sd, cp) {
            return cp(function (p1, p2, p3) {
                return f.apply(this, sd(arguments, d));
            }, f, d);
        }
    };

    function getFnCreator(numParams) {
        if (typeof fnCreators[numParams] != 'undefined')
            return fnCreators[numParams];

        var paramNames = [];
        for (var i = 0; i < numParams; i++) {
            paramNames[i] = "p" + (i + 1);
        }

        fnCreators[numParams] = new Function("f", "d", "sd", "cp",
            "return cp(function(" + paramNames.join(",") + ") {\
                    return f.apply(this, sd(arguments, d));\
                }, f, d);");

        return fnCreators[numParams];
    }

    Function.prototype.WithDefaults = function (defs) {
        var creator = getFnCreator(this.length);
        return creator(this, defs, setDefaults, copyProperties);
    }
})();
