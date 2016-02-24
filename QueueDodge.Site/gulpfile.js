var gulp = require('gulp');
var sass = require('gulp-sass');
var markdown = require('gulp-markdown');

gulp.task('sass', function () {
    gulp.src('./wwwroot/app/styles/style.scss')
        .pipe(sass())
        .pipe(gulp.dest(function (f) {
        return f.base;
    }));
});

gulp.task('markdown', function () {
    return gulp.src('**/*.md')
        .pipe(markdown())
        .pipe(gulp.dest(function (f) {
        return f.base;
    }));
});

gulp.task('default', function () {
    gulp.watch('./wwwroot/app/styles/style.scss', ['sass']);
    gulp.watch('**/*.md', ['markdown']);
    gulp.watch('./wwwroot/**/*.ts', ['typescript']);
});