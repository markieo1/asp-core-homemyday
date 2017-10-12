/// <binding AfterBuild='sass' />
var gulp = require('gulp');
var sass = require("gulp-sass");
var rimraf = require("rimraf");

var paths = {
    webroot: "./wwwroot/"
};

paths.cssFolder = paths.webroot + 'css';

gulp.task("clean:css", function (cb) {
    rimraf(paths.cssFolder, cb);
});

gulp.task("sass", ['clean:css'], function () {
    return gulp.src("./Styles/**/*.scss")
        .pipe(sass())
        .pipe(gulp.dest(paths.cssFolder));
});

gulp.task('sass:watch', function () {
    gulp.watch('./Styles/**/*.scss', ['sass']);
});
