var gulp = require('gulp');
var sass = require('gulp-sass');
var markdown = require('gulp-markdown');
//var typescript = require('gulp-typescript'); // Doesn't work properly.  Use tsc -w


//gulp.task('typescript', function () {
//    var tsProject = typescript.createProject('wwwroot/tsconfig.json', { noResolve: true });
//    var tsResult = tsProject
//        .src()
//        .pipe(typescript(tsProject));

//    return tsResult.js.pipe(gulp.dest(function (f) {
//        return f.base;
//    }));
//});

gulp.task('dependencies', function () {
    gulp.src([
        './node_modules/jquery/dist/jquery.js',
        './node_modules/bootstrap/dist/js/bootstrap.js',
        './node_modules/angular2/bundles/angular2-polyfills.js',
        './node_modules/systemjs/dist/system.src.js',
        './node_modules/rxjs/bundles/Rx.js',
        './node_modules/angular2/bundles/angular2.dev.js',
        './node_modules/angular2/bundles/router.dev.js',
        './node_modules/angular2/bundles/http.dev.js',
        './node_modules/bootstrap/dist/css/bootstrap.css'
    ])
        .pipe(gulp.dest('./wwwroot/vendor/'));
});

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
    gulp.start('dependencies')
    gulp.watch('./wwwroot/app/styles/style.scss', ['sass']);
    gulp.watch('**/*.md', ['markdown']);
});