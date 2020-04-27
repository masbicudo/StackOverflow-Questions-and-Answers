This is my question at TeX Q&A:
[Undesired indentation when using Verbatim inside colorbox inside itemize](https://tex.stackexchange.com/questions/540860/undesired-indentation-when-using-verbatim-inside-colorbox-inside-itemize)

Self answer at TeX Q&A: [2 solutions: unindent content, and unindent whole colorbox](https://tex.stackexchange.com/a/540995/213611)

I will replicate them both here, in this readme.md for reference.

This repo contains the code for both the question, and the answer, and also the generated PDF files for easy visualization of the results.

TeX's:

- [problem.tex](problem.tex)
- [solution1.tex](solution1.tex)
- [solution2.tex](solution2.tex)

PDF's:

- [problem.pdf](problem.pdf)
- [solution1.pdf](solution1.pdf)
- [solution2.pdf](solution2.pdf)

# Question

I want to place a `Verbatim` from `fvextra` package, with a background color, inside a `itemize`.
To accomplish that, I placed the Verbatim inside a `colorbox`.

Unfortunately, there were two problems:

1. there is an undesired indentation inside the colorbox;
2. the background of the colorbox goes beyond the line width

I solved the problem 2 already, using `parbox`, but could not solve the indentation problem.
It seems that Verbatim knows that it is inside an `itemize` environment,
and wants to indent it's content... but the colorbox is already indented as a whole.

Here is a print of the problem:

[![Print of the problem displaying what's wrong!][1]][1]

Is this even possible... maybe a negative margin somehow,
the same size of the current itemize indentation?
But I am really lost here.

Here is the code:

	\documentclass[]{article}
	\usepackage[utf8]{inputenc}
	\usepackage[dvipsnames]{xcolor}
	\definecolor{bgcolor}{cmyk}{0,0.01,0.1,0}
	\usepackage{fvextra}
	\begin{document}
	\begin{itemize}
	\item
		Some text here... I want to keep writing so that the text wraps to the next line.
		This is needed, because I want to see where the line ends, and know what is the line width.
		
		\begin{Verbatim}
	This is OK! But I want a bgcolor...
		\end{Verbatim}
		
		\setbox0\vbox\bgroup
		\begin{Verbatim}
	This has an extra indent, and also the right side goes
	beyond the line width... BAD
		\end{Verbatim}
		\egroup\fboxsep0pt \colorbox{bgcolor}{\usebox0}
		
		\setbox0\vbox\bgroup
		\begin{Verbatim}
	I was unable to find a way to un-indent this text
		\end{Verbatim}
		\egroup\fboxsep0pt \colorbox{bgcolor}{\parbox{\linewidth}{\usebox0}}
	
		Some more text
		
	\begin{itemize}
	\item
		sub text
			
			\setbox0\vbox\bgroup
			\begin{Verbatim}
	Indentation proportional to item level,
	but the colorbox is already indented.
			\end{Verbatim}
			\egroup\fboxsep0pt \colorbox{bgcolor}{\parbox{\linewidth}{\usebox0}}
	\end{itemize}
	
	\end{itemize}
	\end{document}


  [1]: https://i.stack.imgur.com/41uXI.png

# Answer

I figured two possible solutions... don't know if it will
apply for 100% of cases for other people with a similar problem,
because I am no LaTeX expert.

What I did was replace the `Verbatim` environment with
code the that solves all my problems, including the
undesired indentation.

Each of the following codes has a comment like `SOLUTION START`/`SOLUTION END`
to help you see where is the specific code.

## Solution 1: Indented nested Verbatim

This solution consists of adding a negative horizontal spacing
before the real `Verbatim`. It will un-indent the text inside
the colorbox.

To do this, I looked at the source code of `itemize` and `enumerate`,
to find out how they keep track of the nesting level.
They use the variables `\@itemdepth` and `\@enumdepth`.

Also, I found out in this Q&A that I could use `\leftmargin` to
know the size of one indentation level. There is an issue with this,
since I have seen people setting indentation measures
individually for each nesting level. The current solution consists
of multiplying the `\leftmargin` by the sum `\@itemdepth + \@enumdepth`.
It is an issue, but it solves the problem for me.

Here is how it looks:

[![Image displaying how the solution 1 looks][1]][1]

Full code for solution 1:

	\documentclass[]{article}
	\usepackage[utf8]{inputenc}
	
	\usepackage[dvipsnames]{xcolor}
	\definecolor{codebgcolor}{cmyk}{0,0.01,0.1,0}
	
	\usepackage{xfp}
	\makeatletter\def\autogobble{\fpeval{(\@itemdepth+\@enumdepth+1)}}\makeatother
	
	% SOLUTION START
	\usepackage{fvextra}
	\makeatletter
	\let\oldv\Verbatim
	\def\Verbatim{%
		\setbox0\vbox\bgroup\oldv%
	}
	\def\unindent{%
		\hspace{-\dimexpr\leftmargin*(\@itemdepth+\@enumdepth)\relax}%
	}
	\let\oldendv\endVerbatim
	\def\endVerbatim{%
		\oldendv\egroup\fboxsep0pt \par\colorbox{codebgcolor}{%
			\parbox{\linewidth}{%
				\unindent\usebox0%
			}%
		}\par%
	}
	\makeatother
	% SOLUTION END
	
	\begin{document}
		\begin{itemize}
			\item
			Some text here... I want to keep writing so that the text
			wraps to the next line, and we can see what is the line
			width.
			\begin{Verbatim}[gobble=\autogobble]
			This is OK!
			\end{Verbatim}
			Some more text
			\begin{itemize}
				\item
				sub text
				\begin{Verbatim}[gobble=\autogobble]
				Also OK!
				\end{Verbatim}
			\end{itemize}
		\end{itemize}
	\end{document}


## Solution 2: Full width nested Verbatim

This solution consists of eliminating the indentation of a single
paragraph inside the nested `itemize`/`enumerate`, and placing
the `colorbox` inside this paragraph. Since the colorbox already
has its content indended inside of it, the content will appear to
be at the same indentation level, but the colorbox will have the
width of the whole line... the background itself will not be indented.

Here is how it looks:

[![Image displaying how the solution 2 looks][2]][2]

Full code for solution 2:

	\documentclass[]{article}
	\usepackage[utf8]{inputenc}
	
	\usepackage[dvipsnames]{xcolor}
	\definecolor{codebgcolor}{cmyk}{0,0.01,0.1,0}
	
	\usepackage{xfp}
	\makeatletter\def\autogobble{\fpeval{(\@itemdepth+\@enumdepth+1)}}\makeatother
	
	% SOLUTION START
	\usepackage{fvextra}
	\makeatletter
	\let\oldv\Verbatim
	\def\Verbatim{%
		\setbox0\vbox\bgroup\oldv%
	}
	\newcommand\NoIndent[1]{%
		\begingroup\par\parshape0#1\par\endgroup%
	}
	\let\oldendv\endVerbatim
	\def\endVerbatim{%
		\oldendv\egroup\fboxsep0pt \NoIndent{%
			\colorbox{codebgcolor}{%
				\usebox0%
			}%
		}%
	}
	\makeatother
	% SOLUTION END
	
	\begin{document}
		\begin{itemize}
			\item
			Some text here... I want to keep writing so that the text
			wraps to the next line, and we can see what is the line
			width.
			\begin{Verbatim}[gobble=\autogobble]
			This is OK!
			\end{Verbatim}
			Some more text
			\begin{itemize}
				\item
				sub text
				\begin{Verbatim}[gobble=\autogobble]
				Also OK!
				\end{Verbatim}
			\end{itemize}
		\end{itemize}
	\end{document}

### Extra: \autogobble

As an extra, I also found a way to indent the TeX code inside the `Verbatim`,
and remove the indentation when rendering the PDF.

There is an argument `gobble=N` where N is a number, that the `Verbatim`
recognizes, that is used to remove that many N characters from the beginning
of each line.

So I used the `\@itemdepth` and `\@enumdepth` variables to calculate
the amound of nesting needed in TeX code, and made this `\autogobble` command to remove it from the rendered PDF:

	\usepackage{xfp}
	\makeatletter\def\autogobble{\fpeval{(\@itemdepth+\@enumdepth+1)}}\makeatother

  [1]: https://i.stack.imgur.com/tcqgT.png
  [2]: https://i.stack.imgur.com/qlrh2.png